using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.AppUsers
{
    public class AppUserPassword : BaseEntity
    {
        public int AppUserId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether it is current password of user
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// Get or sets the appUser required to change the passowrd
        /// </summary>
        public bool RequiredPasswordChange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the appUser must change passwords after a specified time
        /// </summary>
        public bool EnablePasswordLifetime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the appUser password is expired or not
        /// </summary>
        public DateTime? PasswordExpiredOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual AppUser AppUser { get; set; }

    }
}
