using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Response
{
    public class Authenticate
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string PlayerId { get; set; }
        public string PushToken { get; set; }
    }
}
