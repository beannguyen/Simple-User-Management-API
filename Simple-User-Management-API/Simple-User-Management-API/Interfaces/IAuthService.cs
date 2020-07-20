using Simple_User_Management_API.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Simple_User_Management_API.Interfaces
{
    public interface IAuthService
    {
        bool IsTokenValid(string token, string secretkey);

        string GenerateToken(IAuthContainerModel model);

        IEnumerable<Claim> GetTokenClaims(string token, string secretKey);
    }
}