using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Clean.Infrastructure.Repositories.Repositories.Commons.Extensions
{
    internal static class QueryableExtensions
    {
        public static IQueryable<T> ApplayOrder<T>(this IQueryable<T> queryable, bool isAsc, Expression<Func<T, object>> key) where T : class
            => key == null ? queryable : isAsc ? queryable.AsQueryable().OrderBy(key) : queryable.AsQueryable().OrderByDescending(key);

        public static IQueryable<T> ApplayPagination<T>(this IQueryable<T> queryable, int pageSize, int page) where T : class
            => queryable.AsQueryable().Skip((page - 1) * pageSize).Take(pageSize);

        public static IQueryable<T> ApplayInclude<T>(this IQueryable<T> queryable, params string[] includes) where T : class
        {
            foreach (var include in includes)
                queryable = queryable.Include(include);

            return queryable;
        }

        public static IQueryable<T> ApplayPredict<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> predict, bool isTracking) where T : class
        {
            if (!isTracking)
                queryable = queryable.AsNoTracking();

            queryable = queryable.Where(predict);

            return queryable;
        }
    }
}
