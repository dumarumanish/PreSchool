using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Models
{
    public class AppSettings
    {
        /// <summary>
        /// Jwt token key
        /// </summary>
        public string AuthenticationKey { get; set; }

        /// <summary>
        /// Base url of the hosting api for cold reboot
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Total failed login attempts
        /// </summary>
        public int FailedPasswordAllowedAttempts { get; set; }

        /// <summary>
        /// Total lockout minute time for failed attempts
        /// </summary>
        public int FailedPasswordLockoutMinutes { get; set; }

        /// <summary>
        /// Save login history or not
        /// </summary>
        public bool SaveLoginHistory { get; set; }

        /// <summary>
        /// Total minute for reset password token expiry
        /// </summary>
        public int ResetPasswordExpiryMinutes { get; set; }

        /// <summary>
        /// Enable password life time or not
        /// </summary>
        public bool EnablePasswordLifetime { get; set; }

        /// <summary>
        /// Default password expiry days
        /// </summary>
        public int DefaultPasswordExpiryDays { get; set; }

        public bool ImplementCaptcha { get; set; }

        public string ClientBaseUrl { get; set; }

        /// <summary>
        /// SMS sender from
        /// </summary>
        public string SendSMSFrom { get; set; }
        /// <summary>
        /// SMS Sending access token
        /// </summary>
        public string AccessSMSToken { get; set; }
        /// <summary>
        /// SMS Sending Url.
        /// </summary>
        public string SMSSendUrl { get; set; }
        /// <summary>
        /// it is use to on or off for sending sms.
        /// </summary>
        public bool SendSMS { get; set; }

        /// <summary>
        /// use for cors.
        /// </summary>
        public string[] AllowedOrigins { get; set; }
    }
}
