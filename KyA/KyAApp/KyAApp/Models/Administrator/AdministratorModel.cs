using System;
using Newtonsoft.Json;
using SQLite;

namespace KyAApp.Models.Administrator
{
    public class AdministratorModel
    {
        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty("DateModified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty("IconString")]
        public string IconString { get; set; }

        [JsonProperty("Icon")]
        public byte[] Icon { get; set; }

        [JsonProperty("IdAdmin"),PrimaryKey]
        public Guid IdAdmin { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Status")]
        public bool Status { get; set; }

        [JsonProperty("User")]
        public string User { get; set; }
    }

    public class ListAdministratorModel
    {
        [JsonProperty("Result")]
        public AdministratorModel Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
