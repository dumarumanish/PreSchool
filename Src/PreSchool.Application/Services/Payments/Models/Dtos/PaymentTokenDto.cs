using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Payments.Models.Dtos
{
    public class PaymentTokenDto
    {
        public bool IsSuccess { get; set; }
        public string TokenId { get; set; }
        public string Amount { get; set; }
        public string RefId { get; set; }
    }
}
