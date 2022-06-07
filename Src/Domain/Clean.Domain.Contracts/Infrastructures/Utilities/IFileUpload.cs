using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Contracts.Infrastructures.Utilities
{
    public interface IFileUpload
    {
        Task<string> Upload(byte[] file, string baseAddress, string name);
    }
}
