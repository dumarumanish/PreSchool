using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.PaymentPartners.Models.Commands
{
   public  class VerifyKhaltiPayment
    {
        public string token { get; set; }
        public decimal amount { get; set; }
    }
}
