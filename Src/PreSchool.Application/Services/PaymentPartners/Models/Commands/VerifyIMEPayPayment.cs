using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.PaymentPartners.Models.Commands
{
    public class VerifyIMEPayPayment
    {
        public string RefId { get; set; }
        public string TokenId { get; set; }
        public string TransactionId { get; set; }
        public string Msisdn { get; set; }
    }
}

