using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.PaymentPartners.Models.Commands
{
    public class CardPaymentSignData
    {
        public List<FieldNameValue> Data { get; set; }
    }

    public class FieldNameValue
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }
}
