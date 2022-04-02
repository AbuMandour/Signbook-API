using Newtonsoft.Json;
using SignBookProject.Extensions;
using SignBookProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SignBookProject.Services
{
    public class HttpService : IHttpService
    {
        static readonly HttpClient _httpClient = new HttpClient();
        public async Task<TResponse> SendHttp<TResponse, TRequest>(TRequest requestObejct, Dictionary<string, string> headers, string uri)
        {
            HttpRequestMessage  requestMessage = null;
            HttpResponseMessage responseMessage = null;

            try
            {
                #region Request message
                requestMessage = new HttpRequestMessage();
                // set request message as post
                requestMessage.Method = HttpMethod.Post;
                // add headers to request message
                if (headers != null)
                {                    
                    foreach (var header in headers)
                    {
                        requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                    requestMessage.Headers.TryAddWithoutValidation("Accept", "application/json; charset=utf8");
                }
                //add content to request message
                if (requestObejct != null)
                {
                    var requestJsonAsString = JsonConvert.SerializeObject(requestObejct);
                    var dataContent = new StringContent(requestJsonAsString, Encoding.UTF8, "application/json");
                    requestMessage.Content = dataContent;
                }
                //TODO change to accpect uri not string
                //add uri to request message
                requestMessage.RequestUri = new Uri(uri);
                #endregion

                #region Resoponse message
                responseMessage = await _httpClient.SendAsync(requestMessage);
                var responseJsonAsString = await responseMessage.Content.ReadAsStringAsync();
                var responseObejct = JsonConvert.DeserializeObject<TResponse>(responseJsonAsString);
                return responseObejct;
                #endregion
            }
            catch (Exception exception)
            {
                requestMessage?.Dispose();
                responseMessage?.Dispose();
                Console.WriteLine(exception);                
                throw;
            }
            finally
            {
                requestMessage?.Dispose();
                responseMessage?.Dispose();
            }
        }
    }
}
