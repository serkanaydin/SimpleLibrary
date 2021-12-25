using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using SimpleLibrary.MVC.HttpClient;
using SimpleLibrary.MVC.Jwt;

namespace SimpleLibrary.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpClient, HttpClientAccessor>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.Configure<JwtTokenValidationSettingModel>(this.Configuration.GetSection(nameof(JwtTokenValidationSettingModel)));
            services.AddSingleton<IJwtValidationSettings, JwtValidationSettingsFactory>();

            services.Configure<AuthenticationSettingModel>(this.Configuration.GetSection(nameof(AuthenticationSettingModel)));
            services.AddSingleton<IAuthenticationSettings, AuthenticationSettingsFactory>();
            var serviceProvider = services.BuildServiceProvider();
            var authenticationSettings = serviceProvider.GetService<IAuthenticationSettings>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = authenticationSettings.LoginPath;
                    options.AccessDeniedPath = authenticationSettings.AccessDeniedPath;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                });
            
            services.AddControllersWithViews();
            var handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback += 
                (sender, certificate, chain, errors) =>
                {
                    return true;
                };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
