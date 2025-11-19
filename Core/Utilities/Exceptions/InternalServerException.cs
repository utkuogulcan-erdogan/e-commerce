using System;

namespace Core.Utilities.Exceptions
{
    public class InternalServerException : CustomExceptionBase
    {
        public InternalServerException(string message) : base(message)
        {
        }

        public InternalServerException() : base("An internal server error occurred.")
        {
        }
    }
}
