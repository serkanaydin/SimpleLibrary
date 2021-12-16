using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SimpleLibrary.WebAPI.Middleware;

public class RequiredAttributeMiddleware :ActionFilterAttribute
{

    public override async Task OnResultExecutionAsync(ResultExecutingContext resultExecutingContext, ResultExecutionDelegate next)
    {
        if (resultExecutingContext == null)
            throw new ArgumentNullException(nameof (resultExecutingContext));
        if (next == null)
            throw new ArgumentNullException(nameof (next));
        
        if (resultExecutingContext.ModelState.IsValid is false)
        {
            var states = resultExecutingContext.ModelState;
            var parameters =
                (from p in states
                    select p);
            
            Dictionary<string, string> message = new() { { "Message type", "BadRequest" } };
            
            foreach (var parameterInfo in parameters)
            {
                if (parameterInfo.Value is { ValidationState: ModelValidationState.Invalid })
                    message.Add(parameterInfo.Key, "Invalid parameter");
                
                else if (parameterInfo.Value is { ValidationState: ModelValidationState.Valid })
                    message.Add(parameterInfo.Key, "Valid parameter");
            }

            resultExecutingContext.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await resultExecutingContext.HttpContext.Response.WriteAsJsonAsync(message);
        }
        
        OnResultExecuting(resultExecutingContext);
        if(!resultExecutingContext.HttpContext.Response.HasStarted)
            this.OnResultExecuted(await next());
    }
}