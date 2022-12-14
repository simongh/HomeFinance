using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeFinance.Data
{
	internal class PayeeConfiguration : IEntityTypeConfiguration<Entities.Payee>
	{
		public void Configure(EntityTypeBuilder<Entities.Payee> builder)
		{
			builder.ToTable("Payees", "finance");

			builder.HasId("PayeeId");

			builder
				.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(100)
				.UseCollation("NOCASE");
		}
	}
}