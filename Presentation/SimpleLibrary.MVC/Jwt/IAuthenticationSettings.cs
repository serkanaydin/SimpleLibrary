using Microsoft.AspNetCore.Http;

namespace SimpleLibrary.MVC.Jwt;

public interface IAuthenticationSettings
{
    PathString LoginPath { get; }

    PathString AccessDeniedPath { get; }
}