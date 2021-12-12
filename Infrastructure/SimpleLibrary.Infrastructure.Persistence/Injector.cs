using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleLibrary.Persistence.Repository;

namespace SimpleLibrary.Persistence
{
    public static class RepositoryInjector
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<BookRepository>();
            services.AddTransient<BookTypeRepository>();

            return services;
        }
        public static IServiceCollection AddContext(this IServiceCollection services)
        {
            services.AddDbContext<MainDbContext>();
            services.AddTransient<DbContext, MainDbContext>();
            var serviceProvider = services.BuildServiceProvider();
            
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<MainDbContext>();
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }
            
            return services;
        }
    }
}