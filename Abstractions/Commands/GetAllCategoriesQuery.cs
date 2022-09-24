using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Commands
{
	public class GetAllCategoriesQuery : IRequest<IEnumerable<ResultModels.CategoryResult>>
	{
		public int? Id { get; set; }
	}

	internal class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<ResultModels.CategoryResult>>
	{
		private readonly IDataContext _dataContext;
		private readonly IMapper _mapper;

		public GetAllCategoriesQueryHandler(
			IDataContext dataContext,
			IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ResultModels.CategoryResult>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
		{
			var query = _dataContext.Categories
				.AsNoTracking();

			if (request.Id.HasValue)
				query = query.Where(c => c.ParentId == request.Id);

			return await query
				.OrderBy(c => c.Name)
				.ProjectTo<ResultModels.CategoryResult>(_mapper.ConfigurationProvider)
				.ToArrayAsync();
		}
	}
}