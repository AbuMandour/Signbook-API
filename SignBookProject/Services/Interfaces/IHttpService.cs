using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignBookProject.Services.Interfaces
{
    public interface IHttpService
    {
        Task<TResponse> SendHttp<TResponse, TRequest>(TRequest requestObejct, Dictionary<string, string> headers, string uri);
    }
}
