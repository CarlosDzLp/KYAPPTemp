using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace KyAApp.Models.Owner
{
    public class OwnerModel
    {
        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("IdOwner"),PrimaryKey]
        public Guid? IdOwner { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty("Icon")]
        public byte[] Icon { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("DateModified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty("User")]
        public string User { get; set; }

        [JsonProperty("Status")]
        public bool? Status { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("IdAdmin")]
        public Guid? IdAdmin { get; set; }

        [JsonProperty("IconString")]
        public string IconString { get; set; }
    }
    public class ListOwnerModel
    {
        [JsonProperty("Result")]
        public List<OwnerModel> Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }

    public class Owner
    {
        [JsonProperty("Result")]
        public OwnerModel Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
