﻿using Microsoft.AspNetCore.Diagnostics;
using UserApp.Service.Exceptions;
using UserApp.WebApi.Models;

namespace UserApp.WebApi.Middlewares;

public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        if (exception is not CustomException customException)
            return false;

        await httpContext.Response.WriteAsJsonAsync(new Response
        {
            StatusCode = customException.StatusCode,
            Message = customException.Message,
        });

        return true;
    }
}