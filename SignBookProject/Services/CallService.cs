using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignBookProject.Constants;
using SignBookProject.Models;
using SignBookProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SignBookProject.Services
{
    public class CallService : ICallService
    {
        private readonly IHttpService _httpService;

        public CallService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<CallsResponseModel> CreateUserInSendBirdAsync(CallsRequestModel model)
        {
            var requestHeaders = new Dictionary<string, string>() { { Keys.sendbirdApiTokenKey, Keys.sendbirdApiTokenValue } };
            requestHeaders.Add("Content-Type", "application/json; charset=utf8");
            //TODO create uri
            var callsResponseModel = 
                await _httpService.SendHttp<CallsResponseModel, CallsRequestModel>
                (model, headers: requestHeaders, Uris.SendbirdUri(Keys.applicationId));

            return callsResponseModel;
        }
    }
}
