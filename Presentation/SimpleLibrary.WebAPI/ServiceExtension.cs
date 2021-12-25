using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SimpleLibrary.Application;
using SimpleLibrary.Persistence.Repository;
using SimpleLibrary.WebAPI.Middleware;

namespace SimpleLibrary.WebAPI;

public static class ServiceExtension
{
    public static void AddMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<RequiredAttributeMiddleware>();
    }
    public static void RegisterServiceLayerDi
        (this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IApplicationService>()
            .AddClasses()
            .AsSelf()
            .WithTransientLifetime());
        
        services.Scan(scan => scan
            .FromAssemblyOf<IRepository>()
            .AddClasses()
            .AsSelf()
            .WithTransientLifetime());
    }
}