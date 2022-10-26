using System.Collections.Generic;

namespace HomeFinance.Entities
{
	public class Account
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal OpeningBalance { get; set; }

		public decimal Balance { get; set; }

		public int RowVersion { get; set; }

		public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
	}
}