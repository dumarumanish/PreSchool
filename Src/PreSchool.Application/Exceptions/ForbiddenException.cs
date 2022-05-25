using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PreSchool.Application.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string message = "Forbidden",
             string description = "You don’t have permission to access this resource.") : base(message, description, (int)HttpStatusCode.Forbidden)
        {
        }
    }
}
