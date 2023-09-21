using BokPalace.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Diagnostics;
using System.Net;

namespace BokPalace.Infrastructure.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly IDictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;
    public ExceptionMiddleware()
    {
        _exceptionHandlers = new Dictionary<Type, Func<HttpContext, Exception, Task>>
        {
            { typeof(InvalidOperationException), HandleInvalidOperationExceptionAsync },
            { typeof(ValidationException), HandleValidationExceptionAsync },
            { typeof(NotFoundException), HandleNotFoundExceptionAsync },
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessExceptionAsync },
            //{ typeof(ForbiddenAccessException), HandleForbiddenAccessException },
        };
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        Type type = exception.GetType();
        if (_exceptionHandlers.TryGetValue(type, out Func<HttpContext, Exception, Task>? value))
        {
            await value.Invoke(context, exception);
            return;
        }
    }
    private async Task HandleInvalidOperationExceptionAsync(HttpContext context, Exception exception)
    {
        var invalidOperationexception = (InvalidOperationException)exception;

        // Log the exception details
        Trace.TraceError($"An invalid operation exception occurred: {invalidOperationexception.Message}");
        Log.Error($"An invalid operation exception occurred: {invalidOperationexception.Message}");

        await WriteResponseAsync(context, HttpStatusCode.BadRequest);

    }

    private async Task HandleValidationExceptionAsync(HttpContext context, Exception exception)
    {
        var validationException = (ValidationException)exception;

        Log.Error($"A Validation exception occurred: {exception.Message}");

        var modelState = validationException.Errors
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

        await WriteResponseAsync(context, HttpStatusCode.BadRequest, new { Errors = modelState });

    }
    private async Task HandleNotFoundExceptionAsync(HttpContext context, Exception exception)
    {
        var notFoundexception = (NotFoundException)exception;
        Log.Error($"An Not Found exception occurred: {notFoundexception.Message}");

        await WriteResponseAsync(context, HttpStatusCode.NotFound);

    }
    private async Task HandleUnauthorizedAccessExceptionAsync(HttpContext context, Exception exception)
    {
        Log.Error($"An Unauthorized Access exception occurred: {exception.Message}");

        await WriteResponseAsync(context, HttpStatusCode.Unauthorized);
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Log.Error($"Something went wrong: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }
    private static async Task WriteResponseAsync(HttpContext context, HttpStatusCode statusCode, object? errorResponse = null)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        if (errorResponse != null)
        {
            var json = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}
