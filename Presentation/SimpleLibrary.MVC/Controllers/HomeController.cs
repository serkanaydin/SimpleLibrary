using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleLibrary.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SimpleLibrary.Core.Dtos;
using SimpleLibrary.MVC.HttpClient;

namespace SimpleLibrary.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  System.Net.Http.HttpClient client;
        private Uri baseAddress;

        public HomeController(ILogger<HomeController> logger,IHttpClient client)
        {
            _logger = logger;
            this.client = client.Client;
            baseAddress= new Uri("https://localhost:5001/api/BookType/");
        }

        [HttpGet("Home/Index/{type}/{currentPage}")]
        public async Task<IActionResult> Index(string type,int currentPage)
        {
            SearchByBookTypeResultDto books = null;
            var request = new HttpRequestMessage(HttpMethod.Get, baseAddress +$"get-books?type={type}&currentPage={currentPage}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result =await response.Content.ReadFromJsonAsync<SearchByBookTypeResultDto>();
            if (result != null)
            {
                books = result;
            }
            else 
            {     
                ModelState.AddModelError(string.Empty, "Server error.");
            }
            return View(books);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            BookViewModel input = new BookViewModel();
            return View(input);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel bookViewModel)
        {

            var book = JsonConvert.SerializeObject(bookViewModel);
            var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress +"create-book");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(book, Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            var response = await client.DeleteAsync(id);
            response.EnsureSuccessStatusCode();
            
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(string id)
        {
            BookViewModel book = null;
            var result = await client.GetFromJsonAsync<BookViewModel>(id);
            if (result != null)
            {
                book = result;
            }
            else 
            {     
                ModelState.AddModelError(string.Empty, "Server error.");
            }
            return this.View(book);
        }
        [HttpPost]
        public async Task<IActionResult> Update(BookViewModel bookViewModel)
        {

            var book = JsonConvert.SerializeObject(bookViewModel);
            var request = new HttpRequestMessage(HttpMethod.Put, client.BaseAddress +"update-book");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(book, Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            
            return RedirectToAction("Index");
        }
    }
}
