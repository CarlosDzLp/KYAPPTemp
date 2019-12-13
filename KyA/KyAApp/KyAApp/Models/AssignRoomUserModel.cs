using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace KyAApp.Models
{
    public class AssignRoomUserModel
    {
        [JsonProperty("IdRoom")]
        public Guid? IdRoom { get; set; }

        [JsonProperty("UserId")]
        public Guid? UserId { get; set; }

        [JsonProperty("IdOwner")]
        public Guid? IdOwner { get; set; }

        [JsonProperty("IdAdmin")]
        public Guid? IdAdmin { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty("DateModified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty("StatusAssign")]
        public bool? StatusAssign { get; set; }

        [JsonProperty("IdAssign")]
        public Guid? IdAssign { get; set; }

        [JsonIgnore]
        public string NameRoom { get; set; }
    }
    public class ListAssignRoomUserModel
    {
        [JsonProperty("Result")]
        public List<AssignRoomUserModel> Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
