using System.Text.Json;
using Core.Exceptions;
using Core.Utilities.Exceptions;
using Microsoft.Extensions.Logging;

namespace WepAPI.Middlewares.ExceptionHandlers
{
    public static class ExceptionResponseHelper
    {
        public static (int StatusCode, string Response) HandleException(Exception exception, ILogger logger)
        {
            var statusCode = GetStatusCode(exception);
            var response = CreateResponse(exception, logger, statusCode);
            
            return (statusCode, response);
        }

        private static int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                UnauthorizedException => 401,
                UnauthorizedAccessException => 401,
                BadRequestException => 400,
                ArgumentException => 400,
                ValidationException => 400,
                NotFoundException => 404,
                InternalServerException => 500,
                _ => 500
            };
        }

        private static string CreateResponse(Exception exception, ILogger logger, int statusCode)
        {

            logger.LogError(exception, exception.Message);


            if (exception is ValidationException validationException)
            {
                var validationResult = new
                {
                    Success = false,
                    Message = exception.Message,
                    Errors = validationException.Errors
                };
                return JsonSerializer.Serialize(validationResult);
            }

            var result = new
            {
                Success = false,
                Message = exception.Message
            };

            return JsonSerializer.Serialize(result);
        }
    }
}
