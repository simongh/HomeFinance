using System.Collections.Generic;

namespace HomeFinance.Entities
{
	public class Category
	{
		public int Id { get; init; }

		public string Name { get; set; }

		public string? Description { get; set; }

		public int? ParentId { get; set; }

		public byte[] RowVersion { get; init; } = null!;

		public Category? Parent { get; set; }

		public ICollection<Category> Children { get; set; } = new HashSet<Category>();

		public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
	}
}