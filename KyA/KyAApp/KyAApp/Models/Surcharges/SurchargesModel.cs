using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace KyAApp.Models.Surcharges
{
    public class SurchargesModel
    {
        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("UserId")]
        public Guid? UserId { get; set; }

        [JsonProperty("IdMothly")]
        public Guid? IdMothly { get; set; }

        [JsonProperty("IdAdmin")]
        public Guid? IdAdmin { get; set; }

        [JsonProperty("IdRoom")]
        public Guid? IdRoom { get; set; }

        [JsonProperty("IdOwner")]
        public Guid? IdOwner { get; set; }
       
        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }
       
        [JsonProperty("IdSurcharges")]
        public Guid? IdSurcharges { get; set; }
     
        [JsonProperty("StatusSurcharges")]
        public bool? StatusSurcharges { get; set; }
        
        [JsonProperty("DateModified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty("NameOwner")]
        public string NameOwner { get; set; }

        [JsonProperty("NameUser")]
        public string NameUser { get; set; }

        [JsonProperty("Price")]
        public double? Price { get; set; }

        [JsonProperty("NameRoom")]
        public string NameRoom { get; set; }

        [JsonProperty("DateMonthlyPayment")]
        public DateTime? DateMonthlyPayment { get; set; }
    }

    public class ListSurchargesModel
    {
        [JsonProperty("Result")]
        public List<SurchargesModel> Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
