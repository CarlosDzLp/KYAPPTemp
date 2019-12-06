using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace KyAApp.Models.Rooms
{
    public class RoomsModel
    {
        [JsonProperty("IdOwner")]
        public Guid? IdOwner { get; set; }

        [JsonProperty("IdAdmin")]
        public Guid? IdAdmin { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Price")]
        public double? Price { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty("DateModified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty("IdRoom")]
        public Guid? IdRoom { get; set; }

        [JsonProperty("TypeStatusRoom")]
        public int? TypeStatusRoom { get; set; }

        [JsonProperty("Status")]
        public bool? Status { get; set; }

        [JsonIgnore]
        public string NameOwner { get; set; }

        [JsonIgnore]
        public Guid? Idroom { get; set; }

    }

    public class ListRoomsModel
    {
        [JsonProperty("Result")]
        public List<RoomsModel> Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
