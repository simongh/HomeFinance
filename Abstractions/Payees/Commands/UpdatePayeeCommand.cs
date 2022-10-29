using AutoMapper;
using HomeFinance.Payees.ResultModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Payees.Commands
{
    public class UpdatePayeeCommand : IRequest<PayeeResult>
    {
        public int? Id { get; set; }

        public string Name { get; init; }

        public string? Description { get; init; }
    }

    internal class UpdatePayeeCommandHandler : IRequestHandler<UpdatePayeeCommand, PayeeResult>
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;

        public UpdatePayeeCommandHandler(
            IDataContext dataContext,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<PayeeResult> Handle(UpdatePayeeCommand request, CancellationToken cancellationToken)
        {
            Entities.Payee payee;

            if (request.Id.HasValue)
            {
                payee = await _dataContext.Payees
                    .FirstOrDefaultAsync(p => p.Id == request.Id)
                    ?? throw new NotFoundException($"The payee {request.Id} could not be found");
            }
            else
            {
                payee = new();
                await _dataContext.AddAsync(payee);
            }

            payee.Name = request.Name;
            payee.Description = request.Description;

            await _dataContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PayeeResult>(payee);
        }
    }
}