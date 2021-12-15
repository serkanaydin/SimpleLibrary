#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Extensions;
using SimpleLibrary.Application;
using SimpleLibrary.Core.Dtos;
using SimpleLibrary.Domain;

namespace SimpleLibrary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public BookService bookService;
        public BookController(BookService bookService)
        {
            this.bookService = bookService;
        }
        [HttpGet("get-book-by-name")]
        public async Task<IActionResult> GetBook([FromQuery] string name)
        {
            var result = await bookService.GetBook(name);
            if (result is null)
                return this.Ok(new { message = $"There is no book as named '{name}'"});
            return this.Ok(result);
        }

        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook([FromBody] CreateBookDto model)
        {
            var result = await bookService.AddBook(model);
            return this.Ok(result.GetDisplayName());
        }

        [HttpGet("all-books-by-names")]
        public async Task<IActionResult> GetBooksByNames([FromQuery] string names)
        {
            
            var result = await bookService.GetBooksByNamesAsync(names);
            return Ok(result);
        }
    }
}