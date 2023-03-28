using HackBackend.Data.Infrastructure.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HackBackend.Data.Extensions
{
    public static class OrderByExtensions
    {
        private static readonly MethodInfo OrderByMethod = typeof(Queryable).GetMethods()
            .Where(method => method.Name == "OrderBy").Single(method => method.GetParameters().Length == 2);

        private static readonly MethodInfo OrderByDescendingMethod = typeof(Queryable).GetMethods()
            .Where(method => method.Name == "OrderByDescending").Single(method => method.GetParameters().Length == 2);

        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string propertyName, SortDirection sortDirection)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), null);
            Expression orderByPropertyName = Expression.Property(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(orderByPropertyName, new ParameterExpression[] { parameter });

            MethodInfo genericMethod;
            if (sortDirection == SortDirection.Descending)
            {
                genericMethod = OrderByDescendingMethod.MakeGenericMethod(new Type[] {
                    typeof(TSource),
                    orderByPropertyName.Type
                });
            }
            else
            {
                genericMethod = OrderByMethod.MakeGenericMethod(new Type[] {
                    typeof(TSource),
                    orderByPropertyName.Type
                });
            }

            var orderedResults = genericMethod.Invoke(null, new object[] { source, lambda });

            return (IQueryable<TSource>)orderedResults;
        }
    }
}
