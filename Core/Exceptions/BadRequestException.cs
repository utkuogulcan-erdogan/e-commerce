using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class BadRequestException : CustomExceptionBase
    {
        public BadRequestException(string message) : base(message)
        {
        }
        
        public override int StatusCode => 400;
    }
}
