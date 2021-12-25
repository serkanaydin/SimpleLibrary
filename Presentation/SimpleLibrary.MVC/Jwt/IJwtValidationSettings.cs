using Microsoft.IdentityModel.Tokens;

namespace SimpleLibrary.MVC.Jwt;

public interface IJwtValidationSettings
{
    string SecretKey { get; }

    TokenValidationParameters CreateTokenValidationParameters();
}