using System.ComponentModel.DataAnnotations;

namespace PreSchool.Data.Entities.Payments

{
    /// <summary>
    /// Represents a payment status enumeration
    /// </summary>
    public enum PaymentStatusEnum
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 10,

        /// <summary>
        /// Authorized
        /// </summary>
        Authorized = 20,

        /// <summary>
        /// Paid
        /// </summary>
        Paid = 30,

        /// <summary>
        /// Partially Paid
        /// </summary>
        PartiallyPaid = 35,

       

        /// <summary>
        /// Refunded
        /// </summary>
        Refunded = 40,

        /// <summary>
        /// Partially Refunded
        /// </summary>
        [Display(Name = "Partially Refunded", Description = "Partially Refunded")]
        PartiallyRefunded = 45,

        /// <summary>
        /// Voided
        /// </summary>
        Voided = 50
    }
}
