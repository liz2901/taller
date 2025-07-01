using System.ComponentModel.DataAnnotations;
using System.Net;
using Encuba.Product.Domain.Dtos;
using Encuba.Product.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Encuba.Product.Infrastructure.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogInformation("{Exception} - {Code} - {Message}", nameof(exception), exception.GetHashCode(),
            exception.Message);

        context.Response.ContentType = "application/json";

        context.Response.StatusCode = exception switch
        {
            DomainException domainException => (int)(domainException.HttpStatusCode ?? HttpStatusCode.BadRequest),
            ValidationException _ => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var response = JsonConvert.SerializeObject(new EntityErrorResponse
        {
            Code = exception.GetHashCode(),
            Message = exception.Message,
            Errors = EntityResponseUtils.GetMessageError(exception)
        });

        await context.Response.WriteAsync(response);
    }
}