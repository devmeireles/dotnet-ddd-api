using ChefHero.Application.Common.Exceptions;
using ChefHero.Domain.Common.Exceptions;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ChefHero.API.Common.Exceptions;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        ProblemDetails? problemDetails = exception switch
        {
            DomainValidationException =>
                CreateProblemDetails(
                    StatusCodes.Status400BadRequest,
                    "Validation Error",
                    exception.Message),

            ConflictException =>
                CreateProblemDetails(
                    StatusCodes.Status409Conflict,
                    "Conflict",
                    exception.Message),

            ForbiddenException =>
                CreateProblemDetails(
                    StatusCodes.Status403Forbidden,
                    "Forbidden",
                    exception.Message),

            UnauthorizedAccessException =>
                CreateProblemDetails(
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    "Invalid email or password."),

            _ => null
        };

        if (problemDetails is null)
        {
            _logger.LogError(
                exception,
                "An unhandled exception occurred.");

            problemDetails = CreateProblemDetails(
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred.");
        }

        httpContext.Response.StatusCode =
            problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }

    private static ProblemDetails CreateProblemDetails(
        int status,
        string title,
        string detail)
    {
        return new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail
        };
    }
}