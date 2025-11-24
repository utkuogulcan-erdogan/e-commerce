using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public abstract class CustomExceptionBase : Exception
    {
        public abstract int StatusCode { get; }
        
        public CustomExceptionBase(string message) : base(message)
        {
        }
    }
}
