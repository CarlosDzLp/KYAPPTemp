using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace KyAApp.Models.MonthlyPayment
{
    public class MonthlyPaymentModel
    {
        [JsonProperty("IdRoom")]
        public Guid? IdRoom { get; set; }

        [JsonProperty("UserId")]
        public Guid? UserId { get; set; }

        [JsonProperty("IdOwner")]
        public Guid? IdOwner { get; set; }

        [JsonProperty("IdAdmin")]
        public Guid? IdAdmin { get; set; }

        [JsonProperty("DateMonthlyPayment")]
        public DateTime? DateMonthlyPayment { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [JsonProperty("IdMonthly")]
        public Guid? IdMonthly { get; set; }

        [JsonProperty("NameRoom")]
        public string NameRoom { get; set; }

        [JsonProperty("StatusActivePay")]
        public bool? StatusActivePay { get; set; }

        [JsonProperty("StatusMonthly")]
        public bool? StatusMonthly { get; set; }

        [JsonProperty("NameUser")]
        public string NameUser { get; set; }

        [JsonProperty("Price")]
        public double? Price { get; set; }

        [JsonIgnore]
        public string NameOwner { get; set; }

        [JsonIgnore]
        public string StringStatus { get; set; }

        [JsonIgnore]
        public string Surcharges { get; set; }

        [JsonIgnore]
        public Color ColorMothly { get; set; }
    }
    public class ListMonthlyPaymentModel
    {
        [JsonProperty("Result")]
        public List<MonthlyPaymentModel> Result { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
