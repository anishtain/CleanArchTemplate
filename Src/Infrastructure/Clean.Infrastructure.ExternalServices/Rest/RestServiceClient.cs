using Clean.Domain.Contracts.Infrastructures.ExternalServices;
using Clean.Domain.Resources.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clean.Infrastructure.ExternalServices.Rest
{
    internal class RestServiceClient : IRestServiceClient
    {
        private readonly HttpClient _httpClient;

        public RestServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Post<T, U>(string url, U body, params KeyValuePair<string, string>[] headers)
        {
           
            _httpClient.BaseAddress = new Uri(url);
            foreach (var header in headers)
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

           var data = JsonSerializer.Serialize(body);
            StringContent content = new StringContent(data);

            var response = await _httpClient.PostAsync((string)null,content);

            if (response.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStreamAsync());
            else
                throw new CleanException(Domain.Resources.Exceptions.Commons.ExceptionLevelEnum.Infrastructure, Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.ApiFailed, await response.Content.ReadAsStringAsync());
        }

        public async Task<T> Get<T, U>(string url, U parameters, params KeyValuePair<string, string>[] headers)
        {
            string urlParameters = string.Empty;

            if (parameters != null)
                urlParameters += "?";

            _httpClient.BaseAddress = new Uri(url);
            foreach (var header in headers)
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

            var parametersProperties = parameters.GetType().GetProperties();

            foreach (var property in parametersProperties)
                urlParameters += $"{property.Name}={property.GetValue(parameters)}&";
            urlParameters.Remove(urlParameters.LastIndexOf("&"));

            var response = await _httpClient.GetAsync(urlParameters);

            if (response.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStreamAsync());
            else
                throw new CleanException(Domain.Resources.Exceptions.Commons.ExceptionLevelEnum.Infrastructure, Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.ApiFailed, await response.Content.ReadAsStringAsync());
        }
    }
}
