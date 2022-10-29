using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomeFinance.Payees.ResultModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Payees.Commands
{
	public class GetAllPayeesQuery : IRequest<IEnumerable<PayeeResult>>
	{
	}

	internal class GetAllPayeesQueryHandler : IRequestHandler<GetAllPayeesQuery, IEnumerable<PayeeResult>>
	{
		private readonly IDataContext _dataContext;
		private readonly IMapper _mapper;

		public GetAllPayeesQueryHandler(
			IDataContext dataContext,
			IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<PayeeResult>> Handle(GetAllPayeesQuery request, CancellationToken cancellationToken)
		{
			return await _dataContext.Payees
				.AsNoTracking()
				.OrderBy(c => c.Name)
				.ProjectTo<PayeeResult>(_mapper.ConfigurationProvider)
				.ToArrayAsync();
		}
	}
}