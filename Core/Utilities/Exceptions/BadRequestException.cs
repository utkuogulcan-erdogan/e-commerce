using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Exceptions
{
    public class BadRequestException : CustomExceptionBase
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }
}
