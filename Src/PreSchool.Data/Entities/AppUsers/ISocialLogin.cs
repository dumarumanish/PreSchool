using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.AppUsers
{
    public interface ISocialLogin
    {
        string FacebookUserId { get; set; }
        string GoogleUserId { get; set; }
    }
}
