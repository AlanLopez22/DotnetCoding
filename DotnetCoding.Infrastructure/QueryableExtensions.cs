using System.Linq.Expressions;
using System.Reflection;

namespace DotnetCoding.Infrastructure
{
    public static class QueryableExtensions
    {
        private static MethodInfo StringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;

        public static IQueryable<TEntity> WithTerm<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, string>> selector, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var comparer = GetComparer(selector, value);
                source = source.Where(comparer);
            }

            return source;
        }

        private static Expression<Func<TEntity, bool>> GetComparer<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> selector, TProperty value)
        {
            return GetComparer(selector, value, StringContainsMethod);
        }

        private static Expression<Func<TEntity, bool>> GetComparer<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> selector, TProperty value, MethodInfo methodInfo)
        {
            var parameter = selector.Parameters[0];

            Expression body = Expression.Call(selector.Body, methodInfo, Expression.Constant(value));

            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(body, parameter);

            return lambda;
        }
    }
}
