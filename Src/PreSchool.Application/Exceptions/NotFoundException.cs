using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PreSchool.Application.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message, string description="") : base(message, description, (int)HttpStatusCode.NotFound)
        {
        }
    }
}
