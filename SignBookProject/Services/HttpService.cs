using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SignBookProject.Services
{
    public class HttpService
    {
        public async Task<TResponse> SendHttp<TResponse, TRequest>(TRequest entity,
            Dictionary<string, string> headers, string contentType, string uri)
            where
            TResponse : class
        {
            HttpRequestMessage  RequestMessage = null;
            HttpResponseMessage responseMessage = null;

            HttpClient _httpClient = new HttpClient();

            try
            {
                if (headers != null)
                {
                    _httpClient.DefaultRequestHeaders.Clear();
                    foreach (var header in headers)
                    {
                        _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                StringContent dataContent = null;
                if (entity != null)
                {
                    var json = JsonConvert.SerializeObject(entity);
                    Console.WriteLine(json);
                    dataContent = new StringContent(json, Encoding.UTF8, contentType);
                }

                responseMessage = await _httpClient.PostAsync(uri, dataContent);
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<TResponse>(jsonString);
                return jsonObject;
            }
            catch (Exception exception)
            {
                
                responseMessage?.Dispose();
                throw;
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }
    }
}
