using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomeFinance.Accounts.ResultModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Accounts.Commands
{
	public class GetAccountsQuery : IRequest<IEnumerable<AccountResult>>
	{
		public int? Id { get; set; }
	}

	internal class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, IEnumerable<AccountResult>>
	{
		private readonly IDataContext _dataContext;
		private readonly IMapper _mapper;

		public GetAccountsQueryHandler(IDataContext dataContext,
			IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<AccountResult>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
		{
			var query = _dataContext
				.Accounts
				.AsNoTracking();

			if (request.Id.HasValue)
				query = query.Where(a => a.Id == request.Id);

			return await query
				.OrderBy(a => a.Name)
				.ProjectTo<AccountResult>(_mapper.ConfigurationProvider)
				.ToArrayAsync();
		}
	}
}