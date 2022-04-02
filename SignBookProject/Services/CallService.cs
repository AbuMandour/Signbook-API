using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public CallsResponseModel CreateUserInSendBird(CallsRequestModel model)
        {
            /*
             * Content-Type: application/json; charset=utf8
               Api-Token: {master_api_token or secondary_api_token}
            */
            var client = new HttpClient();
            var httpRequet = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api-0F6F2A82-818A-4A78-A3F3-6776357F9E7D.sendbird.com/v3/users"),
                Headers =
                {
                    { HttpRequestHeader.ContentType.ToString(), "application/json; charset=utf8" },
                    { "Api-Token", "4786a089181ae388ee140f5d6f4a11d059b06092" }
                },
                Content = new StringContent(JsonConvert.SerializeObject(model))
            };
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type:", "application/json; charset=utf8");
            //client.DefaultRequestHeaders.Accept.Add("Content-Type", "application/json; charset=utf8");
            //client.DefaultRequestHeaders.Accept.Add("Api-Token", "4786a089181ae388ee140f5d6f4a11d059b06092");
            //var endPoint = new Uri("https://api-0F6F2A82-818A-4A78-A3F3-6776357F9E7D.sendbird.com/v3/users");
            //var newUser = model;
            //var newUserJson = JsonConvert.SerializeObject(newUser);
            //var payLoad = new StringContent(newUserJson, Encoding.UTF8, "application/json");
            //var result = client.PostAsync(endPoint, payLoad).Result.Content.ReadAsStringAsync().Result;
            var response = client.SendAsync(httpRequet).Result;
            var jsonObject = response.Content.ReadAsStringAsync().Result;
            JObject joRespone = JObject.Parse(jsonObject);
            var accessToken = joRespone.GetValue("access_token").ToString();
            
            return new CallsResponseModel { AccessToken = accessToken };
        }
    }
}
