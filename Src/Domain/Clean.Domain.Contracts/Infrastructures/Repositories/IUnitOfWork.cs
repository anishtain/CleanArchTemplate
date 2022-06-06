using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Contracts.Infrastructures.Repositories
{
    public interface IUnitOfWork
    {
        IBaseRepository<T> GetRepository<T>() where T : class;

        Task Save();
    }
}
