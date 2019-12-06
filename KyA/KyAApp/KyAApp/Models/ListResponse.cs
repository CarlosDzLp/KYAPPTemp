using System;
using Newtonsoft.Json;

namespace KyAApp.Models
{
    public class ListResponse
    {
        [JsonProperty("Result")]
        public bool Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
