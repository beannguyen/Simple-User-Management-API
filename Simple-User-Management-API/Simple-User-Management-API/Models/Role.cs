using System;
using System.Collections.Generic;

namespace Simple_User_Management_API.Models
{
    public class Role
    {
        public Role()
        {
            this.RoleID = Guid.NewGuid();
        }

        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public IList<UserRole> UserRoles { get; set; }
    }
}