using Clean.Domain.Contracts.Infrastructures.Repositories;
using Clean.Domain.Entities.Models.Commons;
using Clean.Infrastructure.Datas.DBContexts;
using Clean.Infrastructure.Repositories.Repositories.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Repositories.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CleanIdentityContext _context;
        private readonly IAuthenticate _authenticate;

        public UnitOfWork(CleanIdentityContext context, IAuthenticate authenticate)
        {
            _context = context;
            _authenticate = authenticate;
        }

        public IBaseRepository<T> GetRepository<T>() where T : class
            => new BaseRepository<T>(_context);

        public async Task Save()
        {
            var currentUser = await _authenticate.GetCurrentUser();

            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Modified when entry.Entity is AuditableEntity entity:
                        entity.UpdateDate = DateTime.Now.Date;
                        entity.UpdatorId = string.IsNullOrEmpty(currentUser) ? currentUser : null;
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Added when entry.Entity is BaseEntity entity:
                        entity.CreationDate = DateTime.Now.Date;
                        entity.CreatorId = string.IsNullOrEmpty(currentUser) ? currentUser : null;
                        break;
                    default:
                        break;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
