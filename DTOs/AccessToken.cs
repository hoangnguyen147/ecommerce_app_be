using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.DTOs
{
    public class AccessToken
    {
        public string user { get; set; }
        public string fullname { get; set; }
        public string avatar { get; set; }
        public string token { get; set; }
    }
}