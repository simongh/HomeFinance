using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Commands
{
	public class UpdateCategoryCommand : IRequest<ResultModels.CategoryResult>
	{
		public int? Id { get; set; }

		public string Name { get; init; }

		public string? Description { get; init; }

		public int? Parent { get; init; }
	}

	internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ResultModels.CategoryResult>
	{
		private readonly IDataContext _dataContext;
		private readonly IMapper _mapper;

		public UpdateCategoryCommandHandler(
			IDataContext dataContext,
			IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}

		public async Task<ResultModels.CategoryResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
		{
			Entities.Category category;

			if (request.Id.HasValue)
			{
				category = (await _dataContext.Categories
					.FirstOrDefaultAsync(c => c.Id == request.Id))
					?? throw new NotFoundException($"Category {request.Id} could not be found");
			}
			else
			{
				category = new();
				await _dataContext.AddAsync(category);
			}

			category.Parent = await _dataContext.ValidateExists<Entities.Category>(c => c.Id, request.Parent, "parent");
			category.Name = request.Name;
			category.Description = request.Description;

			await _dataContext.SaveChangesAsync(cancellationToken);

			return _mapper.Map<ResultModels.CategoryResult>(category);
		}
	}
}