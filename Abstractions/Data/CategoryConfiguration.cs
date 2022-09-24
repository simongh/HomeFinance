using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeFinance.Data
{
	internal class CategoryConfiguration : IEntityTypeConfiguration<Entities.Category>
	{
		public void Configure(EntityTypeBuilder<Entities.Category> builder)
		{
			builder.ToTable("Categories", "finance");

			builder.HasId("CategoryId");
		}
	}
}