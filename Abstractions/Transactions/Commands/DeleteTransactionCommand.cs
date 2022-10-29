using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Transactions.Commands
{
	public class DeleteTransactionCommand : IRequest
	{
		public int Id { get; set; }
	}

	internal class DeleteTransactionCommandHandler : AsyncRequestHandler<DeleteTransactionCommand>
	{
		private readonly IDataContext _dataContext;

		public DeleteTransactionCommandHandler(IDataContext dataContext)
		{
			_dataContext = dataContext;
		}

		protected override async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
		{
			var tx = (await _dataContext.Transactions
				.Where(t => t.Id == request.Id)
				.Include(t => t.Account)
				.Include(t => t.LinkedTransaction)
				.Include(t => t.LinkedTransaction!.Account)
				.FirstOrDefaultAsync())
				?? throw new NotFoundException($"The transaction with ID {request.Id} was not found");

			if (tx.LinkedTransactionId != null)
			{
				tx.LinkedTransaction!.Account.Balance -= tx.Value;
				await _dataContext.RemoveAsync(tx.LinkedTransaction);
			}

			await _dataContext.RemoveAsync(tx);

			tx.Account.Balance -= tx.Value;

			await _dataContext.SaveChangesAsync(cancellationToken);
		}
	}
}