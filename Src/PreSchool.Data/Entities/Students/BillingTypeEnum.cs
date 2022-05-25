using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.Students
{
    public enum BillingTypeEnum
    {
        //used for specific billing cycle/ period

        [Display(Name = "For Billing Period", Description = "For Billing Period")]
        ForBillingPeriod,

        //for ad hoc bill requirement

        [Display(Name = "For Misc. Bills", Description = "For Misc. Bills")]
        ForMiscBills,

    }
}
