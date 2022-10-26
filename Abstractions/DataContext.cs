using HomeFinance.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinance
{
	internal class DataContext : DbContext, IDataContext
	{
		public DbSet<Entities.Account> Accounts { get; set; } = null!;

		public DbSet<Entities.Category> Categories { get; set; } = null!;

		public DbSet<Entities.Payee> Payees { get; set; } = null!;

		public DbSet<Entities.Transaction> Transactions { get; set; } = null!;

		IQueryable<Entities.Account> IDataContext.Accounts => Accounts;

		IQueryable<Entities.Category> IDataContext.Categories => Categories;

		IQueryable<Entities.Payee> IDataContext.Payees => Payees;

		IQueryable<Entities.Transaction> IDataContext.Transactions => Transactions;

		public DataContext(DbContextOptions options)
			: base(options)
		{ }

		public async Task AddAsync<T>(T entity)
		{
			await base.AddAsync(entity);
		}

		public Task AddRangeAsync<T>(IEnumerable<T> entities) => base.AddRangeAsync(entities);

		public Task RemoveAsync<T>(T entity)
		{
			base.Remove(entity);

			return Task.CompletedTask;
		}

		public Task RemoveRangeAsync<T>(IEnumerable<T> entities)
		{
			base.RemoveRange(entities);

			return Task.CompletedTask;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.ApplyConfiguration(new Data.AccountConfiguration())
				.ApplyConfiguration(new Data.CategoryConfiguration())
				.ApplyConfiguration(new Data.PayeeConfiguration())
				.ApplyConfiguration(new Data.TransactionConfiguration());

			modelBuilder.ConfigureRowVersion();

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
	}
}