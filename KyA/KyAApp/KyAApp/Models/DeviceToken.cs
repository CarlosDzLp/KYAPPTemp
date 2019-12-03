using System;
using SQLite;

namespace KyAApp.Models
{
    public class DeviceToken
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string PlayerId { get; set; }
        public string PushToken { get; set; }
    }
}
