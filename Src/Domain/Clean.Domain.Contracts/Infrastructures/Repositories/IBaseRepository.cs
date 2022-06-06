using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Contracts.Infrastructures.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<(int Total, List<T> List)> GetAllAscending(Expression<Func<T, bool>> predictes, bool isTracking, Expression<Func<T, object>> key, params string[] includes);

        Task<(int Total, List<T> List)> GetAllDescending(Expression<Func<T, bool>> predictes, bool isTracking, Expression<Func<T, object>> key, params string[] includes);

        Task<(int Total, List<T> List)> GetPaginationAscending(Expression<Func<T, bool>> predictes, bool isTracking, Expression<Func<T, object>> key, int PageSize, int Page, params string[] includes);

        Task<(int Total, List<T> List)> GetPaginationDescending(Expression<Func<T, bool>> predictes, bool isTracking, Expression<Func<T, object>> key, int PageSize, int Page, params string[] includes);

        Task<T> Get(Expression<Func<T, bool>> predicts, params string[] includes);

        Task<int> Count(Expression<Func<T, bool>> predicts);

        Task<decimal> Sum(Expression<Func<T, bool>> predicts, Expression<Func<T, decimal>> key);

        Task<T> Create(T model);

        void Update(T model);

        void Delete(T model);
    }
}
