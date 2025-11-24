using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class NotFoundException : CustomExceptionBase
    {
        public NotFoundException(string message) : base(message)
        {
        }
        
        public override int StatusCode => 404;
    }
}
