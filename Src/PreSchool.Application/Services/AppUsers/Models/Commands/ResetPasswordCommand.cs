using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Commands
{
    public class ResetPasswordCommand
    {
        [Required]
        public string Username { get; set; }
        public bool SendToEmail { get; set; }
        public bool SendToPhonenumber { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
        public int AppUserTypeId { get; set; }

    }
}
