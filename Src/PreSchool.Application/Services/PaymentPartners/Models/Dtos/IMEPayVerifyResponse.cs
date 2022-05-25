using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.PaymentPartners.Models.Dtos
{
    public class IMEPayVerifyResponse
    {
        public int ResponseCode { get; set; }
        public string Msisdn { get; set; }
        public string TransactionId { get; set; }
        public string ResponseDescription { get; set; }
        public string RefId { get; set; }
        public string TokenId { get; set; }
    }
}
