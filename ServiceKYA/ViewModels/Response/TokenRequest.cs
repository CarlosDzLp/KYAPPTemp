using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Response
{
    public class TokenRequest
    {
        public string Token { get; set; }
        public DateTime Date { get; set; }
        public int Expired { get; set; }
    }
}
