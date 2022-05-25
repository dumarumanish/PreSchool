using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.Stores
{
    /// <summary>
    /// All the store image type, 
    /// Eg : Banner, AdContainer1,AdContainer2,...
    /// </summary>
    public enum StoreImageTypeEnum
    {
        [Display(Name = "Small Logo ", Description = "Small logo of store")]
        SmallLogo = 1,

        [Display(Name = "Medium Logo", Description = "Medium logo of store")]
        MediumLogo,

        [Display(Name = "Large Logo", Description = "Large logo of store")]
        LargeLogo,

        [Display(Name = "Banner", Description = "Banner")]
        Banner ,

        [Display(Name = "Slider", Description = "Slider")]
        Slider,

    }
}
