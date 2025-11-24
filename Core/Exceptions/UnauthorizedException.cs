using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class UnauthorizedException : CustomExceptionBase
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
        
        public override int StatusCode => 401;
    }
}
