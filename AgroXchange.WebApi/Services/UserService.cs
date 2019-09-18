using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using AgroXchange.WebApi.Helpers;
using AgroXchange.WebApi.Models;

namespace AgroXchange.WebApi.Services
{
    //Use https://passwordsgenerator.net/ for strong random strings
    public interface IUserService
    {
        string GenerateToken(Guid userId);

        string GeneratePasswordHash(UserView user, string password);

        bool VerifyPasswordHash(UserView user, string passwordHash, string password);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateToken(Guid userId)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenStr = tokenHandler.WriteToken(token);
            return tokenStr;
        }

        public string GeneratePasswordHash(UserView user, string password)
        {
            user.Password = "";
            return new PasswordHasher<UserView>().HashPassword(user, password + _appSettings.PasswordSalt);
        }

        public bool VerifyPasswordHash(UserView user, string passwordHash, string password)
        {
            user.Password = "";
            return new PasswordHasher<UserView>().VerifyHashedPassword(user, passwordHash, password + _appSettings.PasswordSalt) != PasswordVerificationResult.Failed;
        }
    }
}
