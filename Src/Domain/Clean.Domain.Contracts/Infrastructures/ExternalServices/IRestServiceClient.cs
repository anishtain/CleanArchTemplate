using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Contracts.Infrastructures.ExternalServices
{
    public interface IRestServiceClient
    {
        Task<T> Post<T, U>(string url, U body, params KeyValuePair<string, string>[] headers);

        Task<T> Get<T, U>(string url, U parameters, params KeyValuePair<string, string>[] headers);
    }
}
