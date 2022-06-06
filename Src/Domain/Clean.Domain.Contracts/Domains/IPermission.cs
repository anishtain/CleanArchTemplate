using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Contracts.Domains
{
    public interface IPermission
    {
        public IList<string> Permissions { get; }
    }
}
