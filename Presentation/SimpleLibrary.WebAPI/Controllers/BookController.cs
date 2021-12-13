using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SimpleLibrary.Application;
using SimpleLibrary.Domain.Book;

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
        [HttpGet]
        public Book? GetBook([FromQuery] string name)
        {
            return  bookService.GetBook(name);
        }

        [HttpPost]
        public int AddBook([FromBody] Book book)
        {
            return bookService.AddBook(book);
        }

        [HttpGet("all-books-by-names")]
        public async Task<IActionResult> GetBooksByNames([FromQuery] string names)
        {
            
            var result = await bookService.GetBooksByNamesAsync(names);
            return Ok(result);
        }
    }
}