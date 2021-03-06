using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using SimpleLibrary.Application;
using SimpleLibrary.Core.Dtos.Authentication;
using SimpleLibrary.Core.Enum;

namespace SimpleLibrary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly AccountService _accountService;
        
        public AccountController(AccountService accountService,IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor:httpContextAccessor)
        {
            _accountService = accountService;
        }
        
        [HttpPost]  
        [Route("register")]  
        public async Task<IActionResult> Register([FromBody,Required] RegisterDto model)
        {
            var result = await _accountService.RegisterUser(model);
            return this.Ok(result);
        }
        
        [HttpPost]  
        [Route("login")]  
        public async Task<IActionResult> Login([FromBody,Required] LoginDto model)
        {
            var result = await _accountService.Login(model);
            return this.Ok(result);
        }
        
        [HttpPost]  
        [Route("deactivate-user")]  
        public async Task<IActionResult> DeactivateUser([FromQuery,Required] int userId)
        {
            if (GetUserIdTokenDecode() != userId) 
                return this.Unauthorized(DeactivateResult.UnauthorizedDeactivation);


            var result = await _accountService.DeactivateUser(userId);
            return this.Ok(result);
        }
    }
}