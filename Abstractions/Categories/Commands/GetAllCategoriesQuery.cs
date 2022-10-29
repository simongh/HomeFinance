using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomeFinance.Categories.ResultModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Categories.Commands
{
	public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResult>>
	{
		public int? Id { get; set; }
	}

	internal class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResult>>
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

		public async Task<IEnumerable<CategoryResult>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
		{
			var query = _dataContext.Categories
				.AsNoTracking();

			if (request.Id.HasValue)
				query = query.Where(c => c.ParentId == request.Id);

			return await query
				.OrderBy(c => c.Name)
				.ProjectTo<CategoryResult>(_mapper.ConfigurationProvider)
				.ToArrayAsync();
		}
	}
}