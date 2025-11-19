using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Exceptions
{
    public class CustomExceptionBase : Exception
    {
        public CustomExceptionBase(string message) : base(message)
        {
        }
    }
}
