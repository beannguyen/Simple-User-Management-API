using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Models.ErrorModels
{
    public static class ErrorType
    {
        public static class LoginErrorModel
        {
            public const string ErrorCode = "101";
            public const string Message = "The username or password was not correct";
        }
        public static class PasswordValidationErrorModel
        {
            public const string ErrorCode = "102";
            public const string Message = "Password and Confirm Password are not valid";
        }
        public static class AlreadyExistErrorModel
        {
            public const string ErrorCode = "103";
            public const string Message = "Password and Confirm Password are not valid";
        }
        public static class EmailNotExist 
        {
            public const string ErrorCode = "104";
            public const string Message = "email is not registered";
        }
    }
}
