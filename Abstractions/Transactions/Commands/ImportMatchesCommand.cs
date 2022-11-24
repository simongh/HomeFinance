using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Transactions.Commands
{
    public class ImportMatchesCommand : IRequest
    {
        public int Account { get; set; }

        public MatchModel[]? Matches { get; set; }
    }

    public record MatchModel
    {
        public DateTime Created { get; set; }

        public string? Payee { get; set; }

        public decimal Value { get; set; }

        public bool Keep { get; set; }
    }

    internal class ImportMatchesCommandHandler : AsyncRequestHandler<ImportMatchesCommand>
    {
        private readonly IDataContext _dataContext;

        public ImportMatchesCommandHandler(
            IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override async Task Handle(ImportMatchesCommand request, CancellationToken cancellationToken)
        {
            var account = await (_dataContext.Accounts
                .FirstOrDefaultAsync(a => a.Id == request.Account))
                ?? throw new NotFoundException($"The account with ID {request.Account} could not be found");

            if (request.Matches?.Any() != true)
            {
                return;
            }

            var trx = await Task.WhenAll(request.Matches
                .Where(m => m.Keep)
                .Select(async m => new Entities.Transaction
                {
                    Created = DateOnly.FromDateTime(m.Created),
                    Payee = await FindOrAddPayee(m.Payee),
                    Value = m.Value,
                    Account = account,
                }));

            account.Balance += trx.Sum(m => m.Value);

            await _dataContext.AddRangeAsync(trx);
            await _dataContext.SaveChangesAsync();
        }

        private async Task<Entities.Payee?> FindOrAddPayee(string? payee)
        {
            if (string.IsNullOrEmpty(payee))
                return null;

            var result = await _dataContext.Payees.FirstOrDefaultAsync(p => p.Name == payee);

            if (result == null)
            {
                result = new()
                {
                    Name = payee,
                };
            }

            return result;
        }
    }
}