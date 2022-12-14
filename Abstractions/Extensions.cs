using FluentValidation;
using MediatR;
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

		public static async Task<T?> ValidateExistsAsync<T>(this IDataContext dataContext, Expression<Func<T, int>> queryFn, int? id, string field) where T : class
		{
			if (id == null)
				return null;

			var fn = Expression.Lambda<Func<T, bool>>(Expression.Equal(queryFn.Body, Expression.Constant(id.Value)), queryFn.Parameters[0]);
			var entity = await dataContext.Set<T>().FirstOrDefaultAsync(fn);
			if (entity == null)
				throw new ValidationException(field, $"The {field} with ID {id} could not be found");

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
			services.AddTransient<Services.ISystemClock, Services.SystemClock>();

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviours.ValidationBehaviour<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviours.UnhandledExceptionBeahviour<,>));

			return services;
		}

		public static IServiceCollection AddValidators(this IServiceCollection services)
		{
			services.AddTransient<IValidator<Accounts.Commands.UpdateAccountCommand>, Accounts.Validators.UpdateAccountCommandValidator>();
			services.AddTransient<IValidator<Categories.Commands.UpdateCategoryCommand>, Categories.Validators.UpdateCategoryCommandValidator>();
			services.AddTransient<IValidator<Payees.Commands.UpdatePayeeCommand>, Payees.Validators.UpdatePayeeCommandValidator>();
			services.AddTransient<IValidator<Transactions.Commands.UpdateTransactionCommand>, Transactions.Validators.UpdateTransactionCommandValidator>();

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