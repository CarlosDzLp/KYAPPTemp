using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace KyAApp.Models.User
{
    public class UserModel
    {
        [JsonProperty("IdOwner")]
        public Guid? IdOwner { get; set; }

        [JsonProperty("IdAdmin")]
        public Guid? IdAdmin { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("User")]
        public string User { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("Icon")]
        public byte[] Icon { get; set; }

        [JsonProperty("IconString")]
        public string IconString { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty("DateModified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty("Status")]
        public bool? Status { get; set; }

        [JsonProperty("UserId"),PrimaryKey]
        public Guid? UserId { get; set; }

    }

    public class ListUserModel
    {
        [JsonProperty("Result")]
        public List<UserModel> Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
    public class User
    {
        [JsonProperty("Result")]
        public UserModel Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
    
}
