using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }

    internal class DeleteCategoryCommandHandler : AsyncRequestHandler<DeleteCategoryCommand>
    {
        private readonly IDataContext _dataContext;

        public DeleteCategoryCommandHandler(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _dataContext.Categories
                .Where(c => c.Id == request.Id)
                .Select(c => new
                {
                    Entity = c,
                    InUse = c.Transactions.Any(),
                })
                .FirstOrDefaultAsync()
                ?? throw new NotFoundException($"The category with ID {request.Id} could not be found");

            if (category.InUse)
                throw new ValidationException(nameof(request.Id), "The category is in use and cannot be removed");

            await _dataContext.RemoveAsync(category.Entity);

            await _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}