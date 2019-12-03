using System;
namespace KyAApp.Models.Authenticate
{
    public class AuthenticateModel
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string PlayerId { get; set; }
        public string PushToken { get; set; }
    }
}
