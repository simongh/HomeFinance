using System.Collections.Generic;

namespace HomeFinance.Entities
{
	public class Account
	{
		public int Id { get; init; }

		public string Name { get; set; }

		public decimal OpeningBalance { get; set; }

		public decimal Balance { get; set; }

		public byte[] RowVersion { get; init; } = null!;

		public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
	}
}