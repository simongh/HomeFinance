using AutoMapper;
using AutoMapper.QueryableExtensions;
using CsvHelper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Transactions.Commands
{
    public class MatchCommand : IRequest<IEnumerable<ResultModels.TransactionSummaryResult>>
    {
        public int Account { get; set; }

        public Stream Content { get; set; } = null!;
    }

    internal class MatchCommandHandler : IRequestHandler<MatchCommand, IEnumerable<ResultModels.TransactionSummaryResult>>
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;

        public MatchCommandHandler(
            IDataContext dataContext,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResultModels.TransactionSummaryResult>> Handle(MatchCommand request, CancellationToken cancellationToken)
        {
            var account = await (_dataContext.Accounts
                .AnyAsync(a => a.Id == request.Account));

            if (!account)
                throw new NotFoundException($"The account with ID {request.Account} could not be found");

            var matches = new List<ResultModels.TransactionSummaryResult>();

            using var reader = GetCsvReader(request);
            await foreach (var item in reader.GetRecordsAsync<Maps.ImportModel>())
            {
                var t = await _dataContext.Transactions
                    .Where(t =>
                        t.AccountId == request.Account
                        && t.Created == item.Created
                        && t.Value == item.Value)
                    .ProjectTo<ResultModels.TransactionSummaryResult>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (t.Id == default)
                {
                    t = new ResultModels.TransactionSummaryResult
                    {
                        Created = item.Created,
                        Payee = item.Payee,
                        Value = item.Value,
                    };
                }
                matches.Add(t);
            }

            return matches;
        }

        private IReader GetCsvReader(MatchCommand request)
        {
            var reader = new StreamReader(request.Content);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<Maps.ImportMap>();
            return csv;
        }
    }
}