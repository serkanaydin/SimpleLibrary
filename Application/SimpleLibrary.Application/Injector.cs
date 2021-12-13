using Microsoft.Extensions.DependencyInjection;
using SimpleLibrary.Application;

namespace SimpleLibrary.Persistence
{
    public static class Injector
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<BookService>();
            services.AddTransient<BookTypeService>();
            services.AddTransient<AccountService>();

            return services;
        } 
    }
}