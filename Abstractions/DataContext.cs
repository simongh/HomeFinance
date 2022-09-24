using HomeFinance.Data;
using HomeFinance.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinance
{
	internal class DataContext : DbContext, IDataContext
	{
		public DbSet<Entities.Account> Accounts { get; set; }

		public DbSet<Entities.Category> Categories { get; set; }

		public DbSet<Entities.Payee> Payees { get; set; }

		public DbSet<Entities.Transaction> Transactions { get; set; }

		IQueryable<Account> IDataContext.Accounts => Accounts;

		IQueryable<Category> IDataContext.Categories => Categories;

		IQueryable<Payee> IDataContext.Payees => Payees;

		IQueryable<Transaction> IDataContext.Transactions => Transactions;

		public async Task AddAsync<T>(T entity)
		{
			await base.AddAsync(entity);
		}

		public Task AddRangeAsync<T>(T[] entities) => base.AddRangeAsync(entities);

		public Task RemoveAsync<T>(T entity)
		{
			base.Remove(entity);

			return Task.CompletedTask;
		}

		public Task RemoveRangeAsync<T>(T[] entities)
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
	}
}