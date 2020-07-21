using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Simple_User_Management_API.Models;
using System;
using System.Text;

namespace Simple_User_Management_API.Extension
{
    public static class HashPasswordExtension
    {
        public static string HashPassword(this string password, string secretKey)
        {
           byte[] salt = Encoding.ASCII.GetBytes(secretKey);
           string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password: password,
           salt: salt,
           prf: KeyDerivationPrf.HMACSHA1,
           iterationCount: 10000,
           numBytesRequested: 256 / 8));
           return hashed;
        }
    }
}