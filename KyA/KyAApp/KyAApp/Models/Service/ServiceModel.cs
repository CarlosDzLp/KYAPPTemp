using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace KyAApp.Models.Service
{
    public class ServiceModel
    {
        [JsonProperty("IdOwner")]
        public Guid? IdOwner { get; set; }

        [JsonProperty("IdAdmin")]
        public Guid? IdAdmin { get; set; }

        [JsonProperty("IdService")]
        public Guid? IdService { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Price")]
        public double? Price { get; set; }

        [JsonProperty("Icon")]
        public byte[] Icon { get; set; }

        [JsonProperty("IconString")]
        public string IconString { get; set; }

        [JsonProperty("Status")]
        public bool? Status { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty("DateModified")]
        public DateTime? DateModified { get; set; }

        [JsonIgnore]
        public string NameOwner { get; set; }
    }
    public class ListServiceModel
    {
        [JsonProperty("Result")]
        public List<ServiceModel> Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
