#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        
        [Authorize]
        [HttpGet("get-book-by-name")]
        public async Task<IActionResult> GetBook([FromQuery,Required] string name)
        {
            var result = await bookService.GetBook(name);
            if (result is null)
                return this.Ok(new { message = $"There is no book as named '{name}'"});
            return this.Ok(result);
        }

        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook([FromBody,Required] CreateBookDto model)
        {
            var result = await bookService.AddBook(model);
            return this.Ok(result);
        }

        [HttpGet("all-books-by-names")]
        public async Task<IActionResult> GetBooksByNames([FromQuery,Required] string names)
        {
            var result = await bookService.GetBooksByNamesAsync(names);
            return Ok(result);
        }
    }
}