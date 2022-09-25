using System;
using System.Linq;
using System.Linq.Expressions;

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
	}
}