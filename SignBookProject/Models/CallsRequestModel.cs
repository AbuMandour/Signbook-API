using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Models
{
    public class CallsRequestModel
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("profile_url")]
        public Uri ProfileUrl { get; set; }

        [JsonProperty("issue_access_token")]
        public bool IssueAccessToken { get; set; }

        [JsonProperty("session_token_expires_at")]
        public long SessionTokenExpiresAt { get; set; }

        [JsonProperty("discovery_keys")]
        public List<string> DiscoveryKeys { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("marriage")]
        public string Marriage { get; set; }

        [JsonProperty("hasSomeone")]
        public string HasSomeone { get; set; }
    }
}
