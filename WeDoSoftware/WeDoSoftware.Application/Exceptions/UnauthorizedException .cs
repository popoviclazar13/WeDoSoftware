using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDoSoftware.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message = "You are not authorized to perform this action.") : base(message) { }
    }
}
