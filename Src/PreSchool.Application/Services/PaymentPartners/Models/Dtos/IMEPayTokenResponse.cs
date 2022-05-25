using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.PaymentPartners.Models.Dtos
{
    public class IMEPayTokenResponse
    {
        public int ResponseCode { get; set; }
        public string TokenId { get; set; }
        public string Amount { get; set; }
        public string RefId { get; set; }
    }
}
