using PreSchool.Application.Services.Addresses.Models.Commands;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Students.Models.Commands
{
    public class InsertUpdateFeePayment
    {
        public int Id { get; set; }
        public string TransactionID { get; set; }
        public string ReferenceID { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string ServiceProviderName { get; set; }
        public string Branch { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string Notes { get; set; }
        public decimal Amount { get; set; }
        public decimal? AdditionalCharge { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountAmount { get; set; }

        public decimal TotalAmount { get; set; }
        public PaymentTypeEnum PaymentTypeId { get; set; }
    }
}
