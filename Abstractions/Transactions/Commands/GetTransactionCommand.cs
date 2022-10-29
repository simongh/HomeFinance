using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Transactions.Commands
{
	public class GetTransactionCommand : IRequest<ResultModels.TransactionResult>
	{
		public int Id { get; set; }
	}

	internal class GetTransactionCommandHandler : IRequestHandler<GetTransactionCommand, ResultModels.TransactionResult>
	{
		private readonly IDataContext _dataContext;
		private readonly IMapper _mapper;

		public GetTransactionCommandHandler(
			IDataContext dataContext,
			IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<ResultModels.TransactionResult> Handle(GetTransactionCommand request, CancellationToken cancellationToken)
		{
			var trx = await _dataContext.Transactions
				.AsNoTracking()
				.ProjectTo<ResultModels.TransactionResult>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(t => t.Id == request.Id);

			if (trx.Id != request.Id)
				throw new NotFoundException($"The transaction with ID {request.Id} could not be found");

			return trx;
		}
	}
}