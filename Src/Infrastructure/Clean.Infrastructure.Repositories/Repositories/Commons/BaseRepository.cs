using Clean.Domain.Contracts.Infrastructures.Repositories;
using Clean.Domain.Entities.Models.Commons;
using Clean.Domain.Resources.Exceptions;
using Clean.Infrastructure.Datas.DBContexts;
using Clean.Infrastructure.Repositories.Repositories.Commons.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Repositories.Repositories.Commons
{
    internal class BaseRepository<T>  : IBaseRepository<T> where T : class
    {
        private readonly DbSet<T> _context;

        public BaseRepository(CleanIdentityContext context)
        {
            _context = context.Set<T>();
        }

        public async Task<(int Total, List<T> List)> GetAllAscending(Expression<Func<T, bool>> predictes, bool isTracking, Expression<Func<T, object>> key, params string[] includes)
        {
            var response = _context.ApplayInclude(includes).ApplayPredict(predictes, isTracking).ApplayOrder(true, key);

            return new (await response.CountAsync(), await response.ToListAsync());
        }

        public async Task<(int Total, List<T> List)> GetAllDescending(Expression<Func<T, bool>> predictes, bool isTracking, Expression<Func<T, object>> key, params string[] includes)
        {
            var response = _context.ApplayInclude(includes).ApplayPredict(predictes, isTracking).ApplayOrder(false, key);

            return new (await response.CountAsync(), await response.ToListAsync());
        }

        public async Task<(int Total, List<T> List)> GetPaginationAscending(Expression<Func<T, bool>> predictes, bool isTracking, Expression<Func<T, object>> key, int PageSize, int Page, params string[] includes)
        {
            var response = _context.ApplayInclude(includes).ApplayPredict(predictes, isTracking).ApplayOrder(true, key);

            return new (await response.CountAsync(), await response.ApplayPagination(PageSize, Page).ToListAsync());
        }

        public async Task<(int Total, List<T> List)> GetPaginationDescending(Expression<Func<T, bool>> predictes, bool isTracking, Expression<Func<T, object>> key, int PageSize, int Page, params string[] includes)
        {
            var response = _context.ApplayInclude(includes).ApplayPredict(predictes, isTracking).ApplayOrder(false, key);

            return new (await response.CountAsync(), await response.ApplayPagination(PageSize, Page).ToListAsync());
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicts, params string[] includes)
        {
            var item = await _context.ApplayInclude(includes).ApplayPredict(predicts, false).FirstOrDefaultAsync();

            return item;
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicts)
            => await _context.ApplayPredict(predicts, false).CountAsync();

        public async Task<decimal> Sum(Expression<Func<T, bool>> predicts, Expression<Func<T, decimal>> key)
            => await _context.ApplayPredict(predicts, false).SumAsync(key);

        public async Task<T> Create(T model)
        {
            await _context.AddAsync(model);

            return model;
        }

        public void Update(T model)
        {
            _context.Update(model);
        }

        public void Delete(T model)
        {
            _context.Remove(model);
        }
    }
}
