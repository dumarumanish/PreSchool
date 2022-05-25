using PreSchool.Data.Entities.Files;
using PreSchool.Data.Entities.Schools;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Students
{
    public class FeePayment : CommonProperties
    {
 
        public string TransactionID  { get; set; }
        public string ReferenceID  { get; set; }
        public DateTime? TransactionDate  { get; set; }
        public string ServiceProviderName { get; set; }
        public string Branch { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string Notes { get; set; }
        public decimal Amount { get; set; }
        public decimal? AdditionalCharge  { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalAmount  { get; set; }
        public PaymentTypeEnum PaymentTypeId { get; set; }

    }
}
