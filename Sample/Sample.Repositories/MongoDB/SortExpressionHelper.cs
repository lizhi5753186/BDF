using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Bdf.Domain.Entities;
using Bdf.Domain.Repositories;

namespace Sample.Repositories.MongoDB
{
    public static class SortExpressionHelper
    {
        #region Private Static Methods
        private static IOrderedQueryable<TEntity> InvokeOrderBy<TEntity>(IQueryable<TEntity> query, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
             where TEntity : class, IEntity
        {
            var param = sortPredicate.Parameters[0];
            string propertyName = null;
            Type propertyType = null;
            Expression bodyExpression = null;
            if (sortPredicate.Body is UnaryExpression)
            {
                var unaryExpression = sortPredicate.Body as UnaryExpression;
                bodyExpression = unaryExpression.Operand;
            }
            else if (sortPredicate.Body is MemberExpression)
            {
                bodyExpression = sortPredicate.Body;
            }
            else
                throw new ArgumentException("The body of the sort predicate expression should be either UnaryExpression or MemberExpression.", "sortPredicate");
            var memberExpression = (MemberExpression)bodyExpression;
            propertyName = memberExpression.Member.Name;
            if (memberExpression.Member.MemberType == MemberTypes.Property)
            {
                var propertyInfo = memberExpression.Member as PropertyInfo;
                if (propertyInfo != null) propertyType = propertyInfo.PropertyType;
            }
            else
                throw new InvalidOperationException("Cannot evaluate the type of property since the member expression represented by the sort predicate expression does not contain a PropertyInfo object.");

            var funcType = typeof(Func<,>).MakeGenericType(typeof(TEntity), propertyType);
            var convertedExpression = Expression.Lambda(funcType,
                Expression.Convert(Expression.Property(param, propertyName), propertyType), param);

            var sortingMethods = typeof(Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var sortingMethodName = GetSortingMethodName(sortOrder);
            var sortingMethod = sortingMethods.First(sm => sm.Name == sortingMethodName &&
                                                           sm.GetParameters().Length == 2);
            return (IOrderedQueryable<TEntity>)sortingMethod
                .MakeGenericMethod(typeof(TEntity), propertyType)
                .Invoke(null, new object[] { query, convertedExpression });
        }

        private static string GetSortingMethodName(SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    return "OrderBy";
                case SortOrder.Descending:
                    return "OrderByDescending";
                default:
                    throw new ArgumentException("Sort Order must be specified as either Ascending or Descending.", "sortOrder");
            }
        }
        #endregion

        #region Public Method Extensions
        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a lambda expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the Entity.</typeparam>
        /// <param name="query">A sequence of values to order.</param>
        /// <param name="sortPredicate">The lambda expression which indicates the property for sorting.</param>
        /// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to the lambda expression.</returns>
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, dynamic>> sortPredicate)
            where TEntity : class, IEntity
        {
            return InvokeOrderBy(query, sortPredicate, SortOrder.Ascending);
        }
        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a lambda expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the Entity.</typeparam>
        /// <param name="query">A sequence of values to order.</param>
        /// <param name="sortPredicate">The lambda expression which indicates the property for sorting.</param>
        /// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to the lambda expression.</returns>
        public static IOrderedQueryable<TEntity> OrderByDescending<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, dynamic>> sortPredicate)
            where TEntity : class, IEntity
        {
            return InvokeOrderBy(query, sortPredicate, SortOrder.Descending);
        }
        #endregion
    }
}