using System;
using Newtonsoft.Json;

namespace KyAApp.Models.Tokens
{
    public class TokenRequest
    {
        [JsonProperty("Expired")]
        public int Expired { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }
    }
}
