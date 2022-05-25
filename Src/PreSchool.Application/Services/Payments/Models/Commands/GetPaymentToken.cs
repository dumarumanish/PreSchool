using PreSchool.Application.Services.PaymentPartners.Models.Commands;
using PreSchool.Data.Entities.Payments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Payments.Models.Commands
{
    public class GetPaymentToken
    {
        public PaymentModeEnum PaymentModeId { get; set; }
        [Required]
        public int OrderId { get; set; }

        public List<FieldNameValue> Data { get; set; }

    }
}
