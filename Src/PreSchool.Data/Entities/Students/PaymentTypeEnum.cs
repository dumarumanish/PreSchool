using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.Students
{
    public enum PaymentTypeEnum
    {
        [Display(Name = "Cash", Description = "Cash")]
        Cash,

        [Display(Name = "Cheque", Description = "Cheque")]
        Cheque,

        [Display(Name = "Digital Wallet", Description = "Digital Wallet")]
        DigitalWallet,

        [Display(Name = "Bank Transfer", Description = "Bank Transfer")]
        BankTransfer,

        [Display(Name = "Connect IPS", Description = "Connect IPS")]
        ConnectIPS,

     

    }
}
