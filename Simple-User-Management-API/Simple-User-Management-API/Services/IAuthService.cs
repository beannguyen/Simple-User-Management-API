using Simple_User_Management_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Services
{
    public interface IAuthService
    {
        bool IsTokenValid(string token, string secretkey);
        string GenerateToken(IAuthContainerModel model);
        IEnumerable<Claim> GetTokenClaims(string token, string secretKey);
    }
}
