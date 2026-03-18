using Microsoft.AspNetCore.Mvc;

namespace PrimeraAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception processing request.");

                // Default values
                var status = StatusCodes.Status500InternalServerError;
                var title = "An unexpected error occurred.";
                var detail = "An internal error occurred while processing the request.";

                // Map specific exception types to appropriate HTTP status codes and messages
                switch (ex)
                {
                    case ArgumentNullException _:
                    case ArgumentException _:
                        status = StatusCodes.Status400BadRequest;
                        title = "Invalid request.";
                        detail = ex.Message;
                        break;

                    case KeyNotFoundException _:
                        status = StatusCodes.Status404NotFound;
                        title = "Resource not found.";
                        detail = ex.Message;
                        break;

                    case UnauthorizedAccessException _:
                        status = StatusCodes.Status401Unauthorized;
                        title = "Unauthorized.";
                        detail = "Authentication required or failed.";
                        break;

                    case InvalidOperationException _:
                        status = StatusCodes.Status409Conflict;
                        title = "Conflict.";
                        detail = ex.Message;
                        break;

                    // Add additional cases for domain-specific exceptions as needed
                    default:
                        status = StatusCodes.Status500InternalServerError;
                        title = "An unexpected error occurred.";
                        detail = "An internal error occurred while processing the request.";
                        break;
                }

                var problem = new ProblemDetails
                {
                    Type = $"https://httpstatuses.io/{status}",
                    Title = title,
                    Status = status,
                    Detail = detail,
                    Instance = context.Request.Path
                };

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = status;
                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
