using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomeFinance.Categories.ResultModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Categories.Commands
{
	public class GetCategoryCommand : IRequest<CategoryResult>
	{
		public int Id { get; set; }
	}

	internal class GetCategoryCommandHandler : IRequestHandler<GetCategoryCommand, CategoryResult>
	{
		private readonly IDataContext _dataContext;
		private readonly IMapper _mapper;

		public GetCategoryCommandHandler(
			IDataContext dataContext,
			IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<CategoryResult> Handle(GetCategoryCommand request, CancellationToken cancellationToken)
		{
			var category = await _dataContext.Categories
				.AsNoTracking()
				.ProjectTo<CategoryResult>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(c => c.Id == request.Id);

			if (category.Id != request.Id)
				throw new NotFoundException($"The category with ID {request.Id} could not be found");

			return category;
		}
	}
}