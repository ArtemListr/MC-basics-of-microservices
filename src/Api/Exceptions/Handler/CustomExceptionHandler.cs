using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Exceptions.Handler;

public class CustomExteptionHandler(ILogger<CustomExteptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(

    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken)
    {
        logger.LogError(
            "Ошибки: {exceptionMessage}, время {time}",
            exception.Message,
            DateTime.Now);

        (string Detail, string Title, int StatusCode) details = exception switch
        {
            _ => (exception.Message,
            exception.GetType().Name,
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError)
        };

        var problems = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = httpContext.Request.Path
        };

        if (exception is ValidationException validationException)
        {
            problems.Extensions.Add("Errors", validationException.Errors);
        }


        await httpContext.Response.WriteAsJsonAsync(
                problems,
                cancellationToken: cancellationToken
            );

        return true;
    }
}