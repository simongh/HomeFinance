using CsvHelper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance.Transactions.Commands
{
	public class ImportCsvCommand : IRequest
	{
		public int Account { get; set; }

		public Stream Content { get; set; } = null!;
	}

	internal class ImportCsvCommandHandler : AsyncRequestHandler<ImportCsvCommand>
	{
		private readonly IDataContext _dataContext;

		public ImportCsvCommandHandler(IDataContext dataContext)
		{
			_dataContext = dataContext;
		}

		protected override async Task Handle(ImportCsvCommand request, CancellationToken cancellationToken)
		{
			var account = await (_dataContext.Accounts
				.FirstOrDefaultAsync(a => a.Id == request.Account))
				?? throw new NotFoundException($"The account with ID {request.Account} could not be found");

			using var reader = GetCsvReader(request);
			await foreach (var item in reader.GetRecordsAsync<Maps.ImportModel>())
			{
				var trx = new Entities.Transaction
				{
					Account = account,
					Payee = await FindOrAddPayee(item.Payee),
					Created = item.Created,
					Value = item.Value,
				};
				account.Balance += trx.Value;

				await _dataContext.AddAsync(trx);
			}

			await _dataContext.SaveChangesAsync();
		}

		private IReader GetCsvReader(ImportCsvCommand request)
		{
			var reader = new StreamReader(request.Content);
			var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			csv.Context.RegisterClassMap<Maps.ImportMap>();
			return csv;
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