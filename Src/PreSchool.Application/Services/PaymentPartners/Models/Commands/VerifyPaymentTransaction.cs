using PreSchool.Data.Entities.Payments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.PaymentPartners.Models.Commands
{
    public class VerifyPaymentTransaction
    {
        public PaymentModeEnum PaymentModeId { get; set; }
        [Required]
        public int OrderId { get; set; }
        /// <summary>
        /// Id of the transaction
        /// For esewa: Product code
        /// For Khalti: TransactionId
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Referenc code / token of the transaction
        /// </summary>
        public string TransactionCode { get; set; }

        /// <summary>
        /// Sender unique id
        /// Eg: username / mobile number
        /// </summary>
        public string Sender { get; set; }
    }
}
