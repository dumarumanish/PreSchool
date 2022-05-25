using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Models.PaymentPartners
{
    public class IMEPaySettings
    {
        public string MerchantName { get; set; }
        public string MerchantNumber { get; set; }
        public string MerchantCode { get; set; }
        public string MarchantModule { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }
    }
}
