using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PreSchool.Application.Exceptions
{
    public class FeatureNotAvailableException : BaseException
    {
        public FeatureNotAvailableException(string featureName) : base("Feature not available",
            $"{featureName} is not subscribed.", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
