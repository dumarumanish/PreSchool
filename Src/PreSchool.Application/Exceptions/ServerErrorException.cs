using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PreSchool.Application.Exceptions
{
    public class ServerErrorException : BaseException
    {
        public ServerErrorException(string message, string description = "") : base(message, description, (int)HttpStatusCode.InternalServerError)
        {
        }
    }
}