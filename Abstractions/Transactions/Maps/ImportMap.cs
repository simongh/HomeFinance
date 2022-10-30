using CsvHelper.Configuration;
using System;

namespace HomeFinance.Transactions.Maps
{
	internal class ImportMap : ClassMap<ImportModel>
	{
		public ImportMap()
		{
			AutoMap(System.Globalization.CultureInfo.InvariantCulture);
		}
	}

	internal class ImportModel
	{
		public DateOnly Created { get; set; }

		public string? Payee { get; set; }

		public decimal Value { get; set; }
	}
}