using Clean.Domain.Contracts.Infrastructures.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Utilities.FileUtilities
{
    internal class VideoFileUtility : IFileUpload
    {
        public async Task<string> Upload(byte[] file, string baseAddress, string name)
        {
            string fileName = Path.GetRandomFileName();
            string relativeAddress = $"Videos/{fileName}{Path.GetExtension(name)}";
            string fullAddress = $"{baseAddress}/{relativeAddress}";
            await File.WriteAllBytesAsync(fullAddress, file);

            return relativeAddress;
        }
    }
}
