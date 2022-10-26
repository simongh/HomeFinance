using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeFinance
{
	internal static class Extensions
	{
		public static IQueryable<T> AsOrderedBy<T, TValue>(this IQueryable<T> query, Expression<Func<T, TValue>> expression, string orderBy)
		{
			if (orderBy.EndsWith(".d", StringComparison.OrdinalIgnoreCase))
				return query.OrderByDescending(expression);
			else
				return query.OrderBy(expression);
		}

		public static async Task<T?> ValidateExists<T>(this IDataContext dataContext, Expression<Func<T, int>> queryFn, int? id, string field) where T : class
		{
			if (id == null)
				return null;

			var fn = Expression.Lambda<Func<T, bool>>(Expression.Equal(queryFn, Expression.Constant(id.Value)));
			var entity = await dataContext.Set<T>().FirstOrDefaultAsync(fn);
			if (entity == null)
				throw new ValidationException
				{
					Errors = new[] { $"The {field} with ID {id} could not be found" }
				};

			return entity;
		}
	}

	public static class StartupExtensions
	{
		public static IServiceCollection AddCommands(IServiceCollection services)
		{
			return services;
		}

		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddTransient<IDataContext, DataContext>();

			return services;
		}

		public static IServiceCollection AddDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> action)
		{
			services.AddDbContext<DbContext>(action);

			return services;
		}

		public static void Migrate(this IServiceProvider serviceProvider)
		{
			serviceProvider.GetRequiredService<IDataContext>().Database.Migrate();
		}
	}
}