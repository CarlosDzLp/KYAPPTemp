using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace KyAApp.Models.Message
{
    public class MessageUserToAdmin
    {
        [JsonProperty("UserId")]
        public Guid? UserId { get; set; }

        [JsonProperty("IdAdmin")]
        public Guid? IdAdmin { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("TypeMessage")]
        public int? TypeMessage { get; set; }

        [JsonProperty("TypeSendUser")]
        public int? TypeSendUser { get; set; }

        [JsonProperty("StringImage")]
        public string StringImage { get; set; }

        [JsonProperty("Image")]
        public byte[] Image { get; set; }

        [JsonProperty("MessageID")]
        public Guid? MessageID { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonIgnore]
        public string UrlImage { get; set; }
    }
    public class ListMessageUserToAdmin
    {
        [JsonProperty("Result")]
        public List<MessageUserToAdmin> Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
