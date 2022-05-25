using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.PaymentPartners.Models.Commands
{
    public class IMEPayTransactionToken
    {
        public string Amount { get; set; }
        public string RefId { get; set; }
    }
}
