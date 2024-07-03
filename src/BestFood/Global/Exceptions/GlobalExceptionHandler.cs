using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BestFood.Global.Exceptions;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {
            ValidationException => new ProblemDetails { Status = StatusCodes.Status400BadRequest, Title = "Validation error", Detail = exception.Message },
            FluentValidation.ValidationException validationException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation error",
                Extensions = GetValidationExceptionErrors(validationException)
            },
            _ => new ProblemDetails { Status = StatusCodes.Status500InternalServerError, Title = "Internal server error" }
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
    
    private static Dictionary<string, object?> GetValidationExceptionErrors(FluentValidation.ValidationException exception)
    {
        var resultErrors = new Dictionary<string, object?>();

        foreach (var error in exception.Errors)
            resultErrors.Add(error.PropertyName, error.ErrorMessage);

        return resultErrors;
    }
}