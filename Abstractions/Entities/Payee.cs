﻿using System.Collections.Generic;

namespace HomeFinance.Entities
{
	public class Payee
	{
		public int Id { get; init; }

		public string Name { get; set; }

		public string? Description { get; set; }

		public byte[] RowVersion { get; init; } = null!;

		public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
	}
}