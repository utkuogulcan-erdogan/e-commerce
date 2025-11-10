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
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    Success = false,
                    Message = ex.Message
                };
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    Success = false,
                    Message = ex.Message
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
