using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace KyAApp.Models
{
    public class RoomServiceModel
    {
        [JsonProperty("IdOwner")]
        public Guid? IdOwner { get; set; }

        [JsonProperty("IdAdmin")]
        public Guid? IdAdmin { get; set; }

        [JsonProperty("IdService")]
        public Guid? IdService { get; set; }

        [JsonProperty("IdRoom")]
        public Guid? IdRoom { get; set; }

        [JsonProperty("NameService")]
        public string NameService { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty("IdRoomService")]
        public Guid? IdRoomService { get; set; }

        [JsonProperty("DateModified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty("NameRoom")]
        public string NameRoom { get; set; }

        [JsonProperty("PriceService")]
        public double? PriceService { get; set; }

        [JsonIgnore]
        public string NameOwner { get; set; }

    }

    public class ListRoomServiceModel
    {
        [JsonProperty("Result")]
        public List<RoomServiceModel> Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
