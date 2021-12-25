using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SimpleLibrary.Domain;
using SimpleLibrary.Persistence;

namespace SimpleLibrary.WebAPI.Extensions;

public static class IdentityExtension
{
    public static void AddIdentityConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User,Role>()
            .AddEntityFrameworkStores<MainDbContext>()
            .AddDefaultTokenProviders();
        services.AddScoped<RoleManager<Role>>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>  
        {  
            options.SaveToken = true;  
            options.RequireHttpsMetadata = false;  
            options.TokenValidationParameters = new TokenValidationParameters()  
            {  
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
            };  
        }); 
    }
}