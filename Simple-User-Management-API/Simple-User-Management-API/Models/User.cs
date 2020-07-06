using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Models
{
    public class User
    {
        public User()
        {
            this.UserID = Guid.NewGuid();
        }
        public Guid UserID { get; set; }
        public string UserGoogleID { get; set; }
        public bool UserGoogleAccount { get; set; }
        public string UserName { get; set; }
        public string UserEmailAddress { get; set; }
        public string UserPassword { get; set; }
        public IList<UserRole> UserRoles { get; set; }

    }
}
