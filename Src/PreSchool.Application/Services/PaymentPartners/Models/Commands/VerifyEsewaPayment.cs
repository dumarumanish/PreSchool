using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.PaymentPartners.Models.Commands
{
    public class VerifyEsewaPayment
    {
        public decimal Amount { get; set; }
        public string ProductCode { get; set; }
        public string ReferenceCode { get; set; }
    }
}
