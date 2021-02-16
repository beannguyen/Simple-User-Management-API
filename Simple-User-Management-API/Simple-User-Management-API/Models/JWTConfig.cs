using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Models
{
    public class JWTConfig
    {
        public string Secret { get; set; }
        public string ExpirationInMinutes { get; set; }
    }
}
