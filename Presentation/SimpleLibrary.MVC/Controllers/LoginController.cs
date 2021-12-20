using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NuGet.Protocol;
using SimpleLibrary.Core.Dtos.Authentication;
using SimpleLibrary.MVC.HttpClient;

namespace SimpleLibrary.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private System.Net.Http.HttpClient client;
        private Uri baseAddress;
        public LoginController(ILogger<HomeController> logger,IHttpClient client)
        {
            _logger = logger;
            this.client = client.Client;
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
            var token =await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.JwtToken);

            return Redirect("/Home/Index/laz/1");
        }
    }
}