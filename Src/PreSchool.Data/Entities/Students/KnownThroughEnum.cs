using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.Students
{
    /// <summary>
    /// known about school through.
    /// </summary>
    public enum KnownThroughEnum
    {

        [Display(Name = "Newspaper Ads", Description = "Newspaper Ads")]
        NewspaperAds,

        [Display(Name = "Social Media Ads", Description = "Social Media Ads")]
        SocialMediaAds,

        [Display(Name = "Banner/ Leaflets", Description = "Banner/ Leaflets")]
        BannerOrLeaflets,


        [Display(Name = "Reference", Description = "Reference")]
        Reference,

        [Display(Name = "Others", Description = "Others")]
        Others,

    }
}
