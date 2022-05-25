using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.Payments
{
    public enum PaymentModeEnum
    {
        [Display(Name = "Cash on Delivery", Description = "Cash on delivery")]
        CashOnDelivery = 1,
        
        [Display(Name = "eSewa", Description = "eSewa")]
        Esewa = 2,

        [Display(Name = "Khalti", Description = "Khalti")]
        Khalti = 3,

        [Display(Name = "IMEPay", Description = "IMEPay")]
        IMEPay = 4,

        [Display(Name = "Card", Description = "Card")]
        Card = 5,
    }
}
