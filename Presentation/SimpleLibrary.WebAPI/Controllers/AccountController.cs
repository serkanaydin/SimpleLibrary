using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleLibrary.Application;
using SimpleLibrary.Core.Dtos.Authentication;

namespace SimpleLibrary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]  
        [Route("register")]  
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _accountService.RegisterUser(model);
            if (result is null)
                return this.Ok(new {message = "User couldnt be registered"});
            return this.Ok(result);
        }
        [HttpPost]  
        [Route("login")]  
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _accountService.Login(model);
            if (result is null)
                return this.Unauthorized();
            return this.Ok(result);
        }
    }
}