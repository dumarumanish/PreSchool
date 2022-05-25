using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PreSchool.Application.Exceptions
{
    public class NullException : BaseException
    {
        public NullException(string message, string description = "") : base(message, description, (int) HttpStatusCode.InternalServerError)
        {
        }
    }
}
