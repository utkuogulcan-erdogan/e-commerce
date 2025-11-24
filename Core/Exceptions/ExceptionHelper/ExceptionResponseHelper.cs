using System.Text.Json;
using Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Core.Exceptions.ExceptionHandlers.ExceptionHandlers
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
            if (exception is CustomExceptionBase customExceptionBase)
                return customExceptionBase.StatusCode;
            return 500;
        }

        private static string CreateResponse(Exception exception, ILogger logger, int statusCode)
        {

            logger.LogError(exception, exception.Message);

            var result = new
            {
                Success = false,
                Message = exception.Message
            };

            return JsonSerializer.Serialize(result);
        }
    }
}
