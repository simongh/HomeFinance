using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Commands
{
	public class CategorySummaryQuery : IRequest<IEnumerable<ResultModels.CategorySummaryResult>>
	{
		public DateOnly Start { get; set; }

		public DateOnly End { get; set; }
	}

	internal class CategorySummaryQueryHandler : IRequestHandler<CategorySummaryQuery, IEnumerable<ResultModels.CategorySummaryResult>>
	{
		private readonly IDataContext _dataContext;
		private readonly IMapper _mapper;

		public CategorySummaryQueryHandler(
			IDataContext dataContext,
			IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ResultModels.CategorySummaryResult>> Handle(CategorySummaryQuery request, CancellationToken cancellationToken)
		{
			return (await _dataContext.Transactions
				.Where(t => t.Created >= request.Start && request.End <= request.End)
				.AsNoTracking()
				.Select(t => new
				{
					t.CategoryId,
					Name = t.Category == null ? null : t.Category.Name,
					t.Value,
				})
				.ToArrayAsync())
				.GroupBy(t => new { t.CategoryId, t.Name })
				.Select(g => new ResultModels.CategorySummaryResult
				{
					Name = g.Key.Name,
					Value = g.Sum(t => t.Value),
				});
		}
	}
}