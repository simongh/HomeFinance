using System;

namespace HomeFinance.Entities
{
	public class Transaction
	{
		public int Id { get; set; }

		public DateOnly Created { get; set; }

		public decimal Value { get; set; }

		public string? Memo { get; set; }

		public TransactionStatus Status { get; set; } = TransactionStatus.None;

		public TransactionType Type { get; set; } = TransactionType.None;

		public int RowVersion { get; set; }

		public int? CategoryId { get; set; }

		public int? PayeeId { get; set; }

		public int AccountId { get; set; }

		public Category? Category { get; set; }

		public Payee? Payee { get; set; }

		public Account Account { get; set; }

		public int? LinkedTransactionId { get; set; }

		public Transaction? LinkedTransaction { get; set; }
	}
}