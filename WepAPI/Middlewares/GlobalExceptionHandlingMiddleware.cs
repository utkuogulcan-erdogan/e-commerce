using System.Text.Json;
using WepAPI.Middlewares.ExceptionHandlers;

namespace WepAPI.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, response) = ExceptionResponseHelper.HandleException(exception, _logger);
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            
            return context.Response.WriteAsync(response);
        }
    }
}
