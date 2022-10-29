using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Transactions.Commands
{
	public class GetTransactionsQuery : IRequest<IEnumerable<ResultModels.TransactionSummaryResult>>
	{
		public int AccountId { get; set; }

		public DateOnly? StartDate { get; set; }

		public DateOnly? EndDate { get; set; }

		public string? OrderBy { get; set; }
	}

	internal class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, IEnumerable<ResultModels.TransactionSummaryResult>>
	{
		private readonly IDataContext _dataContext;
		private readonly IMapper _mapper;

		public GetTransactionsQueryHandler(
			IDataContext dataContext,
			IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ResultModels.TransactionSummaryResult>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
		{
			var query = _dataContext.Transactions
				.AsNoTracking()
				.Where(t => t.AccountId == request.AccountId);

			if (request.StartDate.HasValue)
				query = query.Where(t => t.Created > request.StartDate);

			if (request.EndDate.HasValue)
				query = query.Where(t => t.Created < request.EndDate);

			if (request.OrderBy != null)
			{
				if (request.OrderBy.StartsWith("date", StringComparison.OrdinalIgnoreCase))
				{
					query = query.AsOrderedBy(t => t.Created, request.OrderBy);
				}
				else if (request.OrderBy.StartsWith("category", StringComparison.OrdinalIgnoreCase))
				{
					query = query.AsOrderedBy(t => t.Category.Name, request.OrderBy);
				}
				else if (request.OrderBy.StartsWith("payee", StringComparison.OrdinalIgnoreCase))
				{
					query = query.AsOrderedBy(t => t.Payee.Name, request.OrderBy);
				}
			}
			else
				query = query.OrderBy(t => t.Created);

			return await query
				.ProjectTo<ResultModels.TransactionSummaryResult>(_mapper.ConfigurationProvider)
				.ToArrayAsync();
		}
	}
}