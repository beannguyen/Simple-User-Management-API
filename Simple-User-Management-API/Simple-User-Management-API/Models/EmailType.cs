using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Models
{
    public static class EmailType
    {
        public static class ResetPassword
        {
            public const string Subject = "ResetPassword";
            public const string Body = "Your Password: ";
        }
    }
}
