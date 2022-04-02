using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Models
{
    public class CallsRequestModel
    {
        // semd bird model 3 required
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("nickname")]
        public string UserName { get; set; }
        [JsonProperty("profile_url")]
        public string ProfileUrl { get; set; }
    }
}
