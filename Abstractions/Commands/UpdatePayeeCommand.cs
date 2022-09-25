using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Commands
{
	public class UpdatePayeeCommand : IRequest<ResultModels.PayeeResult>
	{
		public int? Id { get; set; }

		public string Name { get; set; }

		public string? Description { get; set; }
	}

	internal class UpdatePayeeCommandHandler : IRequestHandler<UpdatePayeeCommand, ResultModels.PayeeResult>
	{
		private readonly IDataContext _dataContext;
		private readonly IMapper _mapper;

		public UpdatePayeeCommandHandler(
			IDataContext dataContext,
			IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<ResultModels.PayeeResult> Handle(UpdatePayeeCommand request, CancellationToken cancellationToken)
		{
			Entities.Payee payee;

			if (request.Id.HasValue)
			{
				payee = (await _dataContext.Payees
					.FirstOrDefaultAsync(p => p.Id == request.Id))
					?? throw new NotFoundException($"The payee {request.Id} could not be found");
			}
			else
			{
				payee = new();
				await _dataContext.AddAsync(payee);
			}

			payee.Name = request.Name;
			payee.Description = request.Description;

			await _dataContext.SaveChangesAsync(cancellationToken);

			return _mapper.Map<ResultModels.PayeeResult>(payee);
		}
	}
}