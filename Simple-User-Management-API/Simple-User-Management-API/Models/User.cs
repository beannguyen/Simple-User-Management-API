using System;
using System.Collections.Generic;

namespace Simple_User_Management_API.Models
{
    public class User
    {
        public User()
        {
            this.UserID = Guid.NewGuid();
        }

        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; } = "/upload/blank-person.png";
        public IList<UserRole> UserRoles { get; set; }
    }
}