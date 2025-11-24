using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class InternalServerException : CustomExceptionBase
    {
        public InternalServerException(string message) : base(message)
        {
        }
        
        public override int StatusCode => 500;
    }
}
