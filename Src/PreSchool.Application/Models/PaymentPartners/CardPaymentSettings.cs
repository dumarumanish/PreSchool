using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Models.PaymentPartners
{
    public class CardPaymentSettings
    {
        public string MerchantId { get; set; }
        /// <summary>
        /// API Key generated in key management for rest api calls
        /// </summary>
        public string APIKey { get; set; }
        public string APIUrl { get; set; }

        public string SecretKey { get; set; }   
        public string AccessKey { get; set; }
        public string ProfileId { get; set; }
    }
}
