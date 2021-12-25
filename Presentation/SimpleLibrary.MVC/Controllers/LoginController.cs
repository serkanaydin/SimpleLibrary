using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NuGet.Protocol;
using SimpleLibrary.Core.Dtos.Authentication;
using SimpleLibrary.MVC.HttpClient;
using SimpleLibrary.MVC.Jwt;

namespace SimpleLibrary.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;
        private readonly IJwtValidationSettings jwtValidationSettings;
        private System.Net.Http.HttpClient client;
        private Uri baseAddress;
        public LoginController(ILogger<HomeController> logger,IConfiguration configuration,IHttpClient client,IJwtValidationSettings jwtValidationSettings)
        {
            _logger = logger;
            this.client = client.Client;
            this.jwtValidationSettings = jwtValidationSettings;
            this.configuration = configuration;
            baseAddress= new Uri("https://localhost:5001/api/Account/");
        }
        [HttpGet]
        public IActionResult Index()
        {
            LoginDto loginDto = new LoginDto();
            return View(loginDto);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var loginDto = JsonConvert.SerializeObject(login);
            var request = new HttpRequestMessage(HttpMethod.Post, baseAddress +"login");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(loginDto, Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var dto =await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            if (dto != null)
            {
                 var signResult =SignIn(dto);
                var result = User.Identity;
            }
            return Redirect("/Home/Index/laz/1");
        }

        public async Task SignIn(LoginResponseDto response)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.ReadToken(response.JwtToken) as JwtSecurityToken;
            var principal = tokenHandler.ValidateToken(response.JwtToken, this.jwtValidationSettings.CreateTokenValidationParameters(), out var validatedToken);

            var identity = principal.Identity as ClaimsIdentity;
            var extraClaims = token.Claims.Where(c => !identity.Claims.Any(x => x.Type == c.Type)).ToList();
            identity.AddClaims(extraClaims);


            // Search for missed claims, for example claim 'sub'

          
            await this.HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity)).ConfigureAwait(false);
        
        }
    }
}