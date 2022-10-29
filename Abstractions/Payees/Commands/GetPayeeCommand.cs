using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomeFinance.Payees.ResultModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Payees.Commands
{
    public class GetPayeeCommand : IRequest<PayeeResult>
    {
        public int Id { get; set; }
    }

    internal class GetPayeeCommandHandler : IRequestHandler<GetPayeeCommand, PayeeResult>
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;

        public GetPayeeCommandHandler(
            IDataContext dataContext,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<PayeeResult> Handle(GetPayeeCommand request, CancellationToken cancellationToken)
        {
            var payee = await _dataContext.Payees
                .AsNoTracking()
                .ProjectTo<PayeeResult>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            if (payee.Id != request.Id)
                throw new NotFoundException($"The payee with ID {request.Id} could not be found");

            return payee;
        }
    }
}