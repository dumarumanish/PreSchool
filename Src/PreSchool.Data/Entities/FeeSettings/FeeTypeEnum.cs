using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.FeeSettings
{
    /// <summary>
    /// All the fee type, 
    /// Eg : Compulsory Fee, Optional Fees, Miscellaneous Fee,...
    /// </summary>
    public enum FeeTypeEnum
    {
        [Display(Name = "Compulsory Fee", Description = "Compulsory Fee")]
        CompulsoryFee = 1,

        [Display(Name = "Optional Fee", Description = "Optional Fee")]
        OptionalFee,

        [Display(Name = "Miscellaneous Fee", Description = "Miscellaneous Fee")]
        MiscellaneousFee,

    }
}
