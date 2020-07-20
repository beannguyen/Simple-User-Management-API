using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Simple_User_Management_API.Interfaces;
using Simple_User_Management_API.Models;
using Simple_User_Management_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Simple_User_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;
        private IAuthService _jwt;

        public TokenController(IConfiguration config, IAuthService authService)
        {
            _config = config;
            _jwt = authService;
        }

        [HttpPost]
        public string GetRandomToken()
        {
            IAuthContainerModel model = GetJWTContainerModel("Moshe Binieli", "Manager");
            string token = _jwt.GenerateToken(model);

            if (!_jwt.IsTokenValid(token, model.SecretKey))
                throw new UnauthorizedAccessException();
            else
            {
                List<Claim> claims = _jwt.GetTokenClaims(token, model.SecretKey).ToList();
            }
            return token;
        }

        private static JWTContainerModel GetJWTContainerModel(string name, string role)
        {
            return new JWTContainerModel()
            {
                SecretKey = "TW9zaGVFcmV6UHJpdmF0ZUtleQ==",
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Role, role)
                }
            };
        }
    }
}