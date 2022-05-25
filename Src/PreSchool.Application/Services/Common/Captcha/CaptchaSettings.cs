using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Common.Captcha
{
    public class CaptchaSettings
    {
        /// <summary>
        /// Is CAPTCHA enabled?
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// reCAPTCHA public key
        /// </summary>
        public string ReCaptchaPublicKey { get; set; }
        /// <summary>
        /// reCAPTCHA private key
        /// </summary>
        public string ReCaptchaPrivateKey { get; set; }
    }
}
