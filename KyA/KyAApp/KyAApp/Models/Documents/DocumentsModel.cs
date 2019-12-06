using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace KyAApp.Models.Documents
{
    public class DocumentsModel
    {
        [JsonProperty("IdOwner")]
        public Guid? IdOwner { get; set; }

        [JsonProperty("IdAdmin")]
        public Guid? IdAdmin { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("FileDocument")]
        public byte[] FileDocument { get; set; }

        [JsonProperty("StringFile")]
        public string StringFile { get; set; }

        [JsonProperty("Status")]
        public bool? Status { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty("Extensions")]
        public string Extensions { get; set; }

        [JsonProperty("File")]
        public byte[] File { get; set; }

        [JsonProperty("IdDocument")]
        public Guid? IdDocument { get; set; }

        [JsonProperty("DateModified")]
        public DateTime? DateModified { get; set; }

        [JsonIgnore]
        public string Icon { get;  set; }

        [JsonIgnore]
        public string TypeUser { get; set; }
    }

    public class ListDocumentsModel
    {
        [JsonProperty("Result")]
        public List<DocumentsModel> Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
