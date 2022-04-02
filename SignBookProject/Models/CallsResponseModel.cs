using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SignBookProject.Models
{
    public class CallsResponseModel
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("profile_url")]
        public Uri ProfileUrl { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("is_online")]
        public bool IsOnline { get; set; }

        [JsonProperty("last_seen_at")]
        public long LastSeenAt { get; set; }

        [JsonProperty("discovery_keys")]
        public List<string> DiscoveryKeys { get; set; }

        [JsonProperty("preferred_languages")]
        public List<object> PreferredLanguages { get; set; }

        [JsonProperty("has_ever_logged_in")]
        public bool HasEverLoggedIn { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public class MetadataResponse
    {
        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("marriage")]
        public string Marriage { get; set; }

        [JsonProperty("hasSomeone")]
        public string HasSomeone { get; set; }
    }
}
