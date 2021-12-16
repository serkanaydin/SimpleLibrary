using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SimpleLibrary.WebAPI.Middleware;

namespace SimpleLibrary.WebAPI;

public static class ServiceExtension
{
    public static void AddMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<RequiredAttributeMiddleware>();
    }
}