using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleLibrary.Core.Dtos.Authentication;
using SimpleLibrary.Core.Enum;
using SimpleLibrary.Domain;

namespace SimpleLibrary.Core.Helper.Authentication
{
    public class JwtHelper
    { 
        public static LoginResponseDto generateJwtToken(User user,IConfiguration configuration)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new LoginResponseDto()
                { result = UserEnums.SuccessfulLogin,ExpiresAt = tokenDescriptor.Expires, JwtToken = tokenHandler.WriteToken(token) };
        }
    }
}