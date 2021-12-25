using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace SimpleLibrary.MVC.Jwt;

public class AuthenticationSettingsFactory : IAuthenticationSettings
{
    public AuthenticationSettingsFactory(IConfiguration configuration)
    {
        this.LoginPath = new PathString(configuration["Jwt:Login"]);
        this.AccessDeniedPath = default(PathString);
    }

    public PathString AccessDeniedPath { get; set; }

    public PathString LoginPath { get;  set; }
}