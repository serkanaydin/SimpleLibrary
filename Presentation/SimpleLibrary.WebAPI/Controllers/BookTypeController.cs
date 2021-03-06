using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using SimpleLibrary.Application;
using SimpleLibrary.Core.Dtos;
using SimpleLibrary.WebAPI.Middleware;

namespace SimpleLibrary.WebAPI.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(RequiredAttributeMiddleware))]
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
            var result = await _bookTypeService.CreateBookType(model);
            return this.Ok(result);
        }
        
        [HttpDelete("delete-booktype")]
        public async Task<IActionResult> DeleteBookType([FromQuery,Required]int typeId)
        {
            var result = await _bookTypeService.DeleteBookType(typeId);
            return this.Ok(result);
        }
        [HttpGet("get-books")]
        public async Task<IActionResult> GetBooks([FromQuery,Required] string type,[FromQuery,Required,Range(0,int.MaxValue)]int currentPage)
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