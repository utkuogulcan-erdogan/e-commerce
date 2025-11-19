using System;

namespace Core.Utilities.Exceptions
{
    public class UnauthorizedException : CustomExceptionBase
    {
        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException() : base("Unauthorized access.")
        {
        }
    }
}
