using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Commands
{
	public class UpdateAccountCommand : IRequest<ResultModels.AccountResult>
	{
		public int? Id { get; set; }

		public string Name { get; set; } = null!;

		public decimal OpeningBalance { get; set; }
	}

	internal class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, ResultModels.AccountResult>
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

		public async Task<ResultModels.AccountResult> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
		{
			Entities.Account? account;

			if (request.Id.HasValue)
			{
				account = (await _dataContext.Accounts
					.FirstOrDefaultAsync(a => a.Id == request.Id))
					?? throw new NotFoundException($"Account {request.Id} could not be found");
			}
			else
			{
				account = new();
				await _dataContext.AddAsync(account);
			}

			account.Balance += account.OpeningBalance - request.OpeningBalance;
			account.Name = request.Name;

			await _dataContext.SaveChangesAsync(cancellationToken);

			return _mapper.Map<ResultModels.AccountResult>(account);
		}
	}
}