using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleLibrary.WebAPI.Controllers;

public abstract class BaseController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    protected BaseController(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    protected long GetUserIdTokenDecode()
    {
        var id = _httpContextAccessor?.HttpContext?.User?.FindFirst("id")?.Value;
        return Convert.ToInt64(id);
    }
}