using AutoMapper;
using HomeFinance.Accounts.ResultModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Accounts.Commands
{
	public class UpdateAccountCommand : IRequest<AccountResult>
	{
		public int? Id { get; set; }

		public string Name { get; init; } = null!;

		public decimal OpeningBalance { get; init; }
	}

	internal class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountResult>
	{
		private readonly IDataContext _dataContext;
		private readonly IMapper _mapper;

		public UpdateAccountCommandHandler(
			IDataContext dataContext,
			IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<AccountResult> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
		{
			Entities.Account? account;

			if (request.Id.HasValue)
			{
				account = await _dataContext.Accounts
					.FirstOrDefaultAsync(a => a.Id == request.Id)
					?? throw new NotFoundException($"Account {request.Id} could not be found");
			}
			else
			{
				account = new();
				await _dataContext.AddAsync(account);
			}

			account.Balance = account.Balance - account.OpeningBalance + request.OpeningBalance;
			account.OpeningBalance = request.OpeningBalance;
			account.Name = request.Name;

			await _dataContext.SaveChangesAsync(cancellationToken);

			return _mapper.Map<AccountResult>(account);
		}
	}
}