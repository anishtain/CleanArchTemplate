using Clean.Domain.Contracts.Infrastructures.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Utilities.FileUtilities
{
    internal class FileUtility : IFileUtility
    {
        private readonly IFileUpload _fileUpload;

        public FileUtility(IFileUpload fileUpload)
        {
            _fileUpload = fileUpload;
        }

        public async Task<string> Upload(byte[] file, string baseAddress, string name)
        {
            return await _fileUpload.Upload(file, baseAddress, name);
        }
    }
}
