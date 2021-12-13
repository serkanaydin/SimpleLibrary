using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleLibrary.Application;
using SimpleLibrary.Core.Dtos;

namespace SimpleLibrary.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookTypeController : ControllerBase
    {
        private readonly BookTypeService _bookTypeService;
        public BookTypeController(BookTypeService bookTypeService)
        {
            _bookTypeService = bookTypeService;
        }
        
        [HttpPost("add-booktype")]
        public async Task<IActionResult> CreateBookType(BookTypeModelDto model)
        {
            var result = await _bookTypeService.CreateBookType(model);
            return this.Ok(result);
        }
        
        [HttpDelete("delete-booktype")]
        public async Task<IActionResult> DeleteBookType(string type)
        {
            var result = await _bookTypeService.DeleteBookType(type);
            if (result is null)
                return this.Ok(
                    new
                    {
                        IsSuccess = false,
                        Message = $"There is no BookType as {type}"
                    });
            return this.Ok(new{isSuccess=true});
        }
        
        [HttpGet("get-books")]
        public async Task<IActionResult> GetBooks([FromQuery] string type,[FromQuery]int currentPage)
        {
            var result =await _bookTypeService.GetBooksByBookType(type, currentPage);
            if (result is null)
            {
                return this.Ok(new
                {
                    IsSuccess=false,
                    Message=$"There is no BookType as {type}"
                });
            } 
            return this.Ok(result);
        }
    }
}