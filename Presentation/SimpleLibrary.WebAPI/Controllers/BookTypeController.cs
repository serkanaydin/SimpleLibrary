using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Application;
using SimpleLibrary.Core.Dtos;

namespace SimpleLibrary.WebAPI.Controllers
{
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
        public async Task<IActionResult> CreateBookType([FromBody,Required]BookTypeModelDto model)
        {
            if (ModelState.IsValid is false)
                return BadRequest();
            
            var result = await _bookTypeService.CreateBookType(model);
            return this.Ok(result);
        }
        
        [HttpDelete("delete-booktype")]
        public async Task<IActionResult> DeleteBookType([FromQuery,Required]string type)
        {
            if (ModelState.IsValid is false)
                return BadRequest();
            
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
        public async Task<IActionResult> GetBooks([FromQuery,Required] string type,[FromQuery,Required]int currentPage)
        {
            if (ModelState.IsValid is false)
                return BadRequest();
            
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