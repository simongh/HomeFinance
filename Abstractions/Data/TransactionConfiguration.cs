using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeFinance.Data
{
	internal class TransactionConfiguration : IEntityTypeConfiguration<Entities.Transaction>
	{
		public void Configure(EntityTypeBuilder<Entities.Transaction> builder)
		{
			builder.ToTable("Transactions", "finance");

			builder.HasId("TransactionId");
		}
	}
}