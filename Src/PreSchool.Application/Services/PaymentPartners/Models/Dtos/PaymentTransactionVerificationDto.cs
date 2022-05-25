using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.PaymentPartners.Models.Dtos
{
    public class PaymentTransactionVerificationDto
    {
        public bool IsPaymentVerified { get; set; }
        public string Remark { get; set; }
    }
}
