using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Models.ErrorModels
{
    public class ErrorModel
    {
        public ErrorModel(string ErrorCode,string Message)
        {
            this.ErrorCode = ErrorCode;
            this.Message = Message;
        }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
