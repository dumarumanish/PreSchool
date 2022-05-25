using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PreSchool.Application.Exceptions
{
    public class InvalidException : BaseException
    {
        public InvalidException(string message, string description = "") : base(message, description, (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
