using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Models
{
    public class UserRole
    {
        public Guid RoleID { get; set; }
        public Role Role { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; }
    }
}
