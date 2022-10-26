using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeFinance
{
	internal interface IDataContext
	{
		IQueryable<Entities.Account> Accounts { get; }
		IQueryable<Entities.Category> Categories { get; }
		IQueryable<Entities.Payee> Payees { get; }
		IQueryable<Entities.Transaction> Transactions { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

		Task AddAsync<T>(T entity);

		Task AddRangeAsync<T>(IEnumerable<T> entities);

		Task RemoveAsync<T>(T entity);

		Task RemoveRangeAsync<T>(IEnumerable<T> entities);

		DbSet<T> Set<T>() where T : class;

		DatabaseFacade Database { get; }
	}
}