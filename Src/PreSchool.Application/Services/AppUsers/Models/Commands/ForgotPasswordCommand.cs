using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Commands
{
    public class ForgotPasswordCommand
    {
        public string Username { get; set; }
        public bool SendToEmail { get; set; }
        public bool SendToPhonenumber { get; set; }
        public int AppUserTypeId { get; set; }

    }
}
