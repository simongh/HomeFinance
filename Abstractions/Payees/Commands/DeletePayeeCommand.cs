using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Payees.Commands
{
	public class DeletePayeeCommand : IRequest
	{
		public int Id { get; set; }
	}

	internal class DeletePayeeCommandHandler : AsyncRequestHandler<DeletePayeeCommand>
	{
		private readonly IDataContext _dataContext;

		public DeletePayeeCommandHandler(IDataContext dataContext)
		{
			_dataContext = dataContext;
		}

		protected override async Task Handle(DeletePayeeCommand request, CancellationToken cancellationToken)
		{
			var payee = await _dataContext.Payees
				.Where(p => p.Id == request.Id)
				.Select(p => new
				{
					Entity = p,
					InUse = p.Transactions.Any(),
				})
				.FirstOrDefaultAsync()
				?? throw new NotFoundException($"The payee with ID {request.Id} could nto be found");

			if (payee.InUse)
				throw new ValidationException(nameof(request.Id), "The payee is in use and cannot be removed");

			await _dataContext.RemoveAsync(payee.Entity);
			await _dataContext.SaveChangesAsync(cancellationToken);
		}
	}
}