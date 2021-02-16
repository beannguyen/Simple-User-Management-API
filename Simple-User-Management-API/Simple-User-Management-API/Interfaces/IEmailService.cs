using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Interfaces
{
    public interface IEmailService
    {
        void Send(string ToEmail, string Subject, string Body);
    }
}
