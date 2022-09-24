using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeFinance.Data
{
	internal class AccountConfiguration : IEntityTypeConfiguration<Entities.Account>
	{
		public void Configure(EntityTypeBuilder<Entities.Account> builder)
		{
			builder.ToTable("Accounts", "finance");

			builder.HasId("AccountId");
		}
	}
}