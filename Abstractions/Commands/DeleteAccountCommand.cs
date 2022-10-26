using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Commands
{
	public class DeleteAccountCommand : IRequest
	{
		public int Id { get; set; }
	}

	internal class DeleteAccountCommandHandler : AsyncRequestHandler<DeleteAccountCommand>
	{
		private readonly IDataContext _dataContext;

		public DeleteAccountCommandHandler(
			IDataContext dataContext)
		{
			_dataContext = dataContext;
		}

		protected override async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
		{
			var account = (await _dataContext.Accounts
				.FirstOrDefaultAsync(a => a.Id == request.Id))
				?? throw new NotFoundException($"Account with ID {request.Id} was not found");

			var trx = _dataContext.Transactions.Where(t => t.AccountId == request.Id);
			if (await trx.AnyAsync())
			{
				await _dataContext.RemoveRangeAsync(await trx.ToArrayAsync());
			}

			await _dataContext.RemoveAsync(account);

			await _dataContext.SaveChangesAsync(cancellationToken);
		}
	}
}