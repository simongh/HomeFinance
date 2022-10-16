using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Commands
{
	public class UpdateTransactionCommand : IRequest<ResultModels.TransactionResult>
	{
		public int? Id { get; set; }

		public int? Category { get; init; }

		public int Account { get; init; }

		public int? Payee { get; init; }

		public DateOnly Created { get; init; }

		public TransactionType Type { get; init; }

		public TransactionStatus Status { get; init; }

		public decimal Value { get; init; }

		public int? TransferAccount { get; init; }
	}

	internal class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, ResultModels.TransactionResult>
	{
		private readonly IDataContext _dataContext;
		private readonly IMapper _mapper;

		public UpdateTransactionCommandHandler(
			IDataContext dataContext,
			IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<ResultModels.TransactionResult> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
		{
			Entities.Transaction trx;

			if (request.Id.HasValue)
			{
				trx = (await _dataContext.Transactions
					.FirstOrDefaultAsync(t => t.Id == request.Id.Value))
					?? throw new NotFoundException($"The transaction ID {request.Id} was not found");
			}
			else
			{
				trx = new();

				var account = (await _dataContext.Accounts
					.FirstOrDefaultAsync(a => a.Id == request.Account))
					?? throw new NotFoundException($"The account ID {request.Id} was not found");

				account.Transactions.Add(trx);
			}

			await _dataContext.ValidateExists<Entities.Category>(c => c.Id, request.Category, "category");
			await _dataContext.ValidateExists<Entities.Payee>(p => p.Id, request.Payee, "payee");

			trx.Status = request.Status;
			trx.Created = request.Created;
			trx.CategoryId = request.Category;
			trx.PayeeId = request.Payee;
			trx.Type = request.Type;
			trx.Value = request.Value;

			await HandleTransferAsync(request, trx);

			await _dataContext.SaveChangesAsync();

			return _mapper.Map<ResultModels.TransactionResult>(trx);
		}

		private async Task HandleTransferAsync(UpdateTransactionCommand request, Entities.Transaction transaction)
		{
			var transferTo = await _dataContext.ValidateExists<Entities.Account>(a => a.Id, request.TransferAccount, "target account");
			if (transferTo != null)
			{
				var targetTrx = await FindUpdateLinkedAsync(transaction, transferTo);

				if (transaction.LinkedTransactionId == null)
				{
					targetTrx = new()
					{
						Created = transaction.Created,
						Payee = transaction.Payee,
						LinkedTransaction = transaction,
						Account = transferTo,
					};

					transaction.LinkedTransaction = targetTrx;
				}

				targetTrx!.Value = transaction.Value;
			}
			else
			{
				await FindUpdateLinkedAsync(transaction, null);
			}
		}

		private async Task<Entities.Transaction?> FindUpdateLinkedAsync(Entities.Transaction transaction, Entities.Account? account)
		{
			if (transaction.LinkedTransactionId == null)
				return null;

			var targetTrx = await _dataContext.Transactions.FirstAsync(t => t.Id == transaction.LinkedTransactionId);

			if (targetTrx.AccountId == account?.Id)
				return targetTrx;

			targetTrx.LinkedTransactionId = null;
			transaction.LinkedTransactionId = null;

			return null;
		}
	}
}