using System;
using System.Globalization;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SimpleLibrary.MVC.Jwt;


    public class JwtValidationSettingsFactory : IJwtValidationSettings
    {
        private readonly IConfiguration configuration;

        public JwtValidationSettingsFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string SecretKey => this.configuration["Jwt:Secret"];
        

        public TokenValidationParameters CreateTokenValidationParameters()
        {
            var result = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.SecretKey)),
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            return result;
        }
    
}