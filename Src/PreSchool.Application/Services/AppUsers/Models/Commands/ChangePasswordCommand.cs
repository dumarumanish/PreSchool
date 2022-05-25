using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Commands
{
    public class ChangePasswordCommand
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        /// <summary>
        /// Required to change the passowrd
        /// </summary>
        public bool RequiredPasswordChange { get; set; }

        /// <summary>
        /// Indicating whether the appUser must change passwords after a specified time
        /// </summary>
        public bool EnablePasswordLifetime { get; set; }

        /// <summary>
        /// If EnablePasswordLifetime then password expiry date
        /// </summary>
        public DateTime? PasswordExpiredOn { get; set; }

        public int AppUserTypeId { get; set; }

    }
}
