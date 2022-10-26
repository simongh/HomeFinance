using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeFinance.Data
{
	internal static class Extensions
	{
		public static void HasId<T>(this EntityTypeBuilder<T> builder, string name) where T : class
		{
			builder.Property("Id").HasColumnName(name).ValueGeneratedOnAdd();
		}

		public static void ConfigureRowVersion(this ModelBuilder modelBuilder)
		{
			var types = modelBuilder.Model.GetEntityTypes();

			foreach (var item in types)
			{
				var p = item.GetProperty("RowVersion");

				if (p == null)
					continue;

				p.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAddOrUpdate;
				p.IsConcurrencyToken = true;
				p.SetDefaultValue(0);
			}
		}
	}
}