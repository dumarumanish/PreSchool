using PreSchool.Data.Entities.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Stores
{
    public class StoreSocialMedia : CommonProperties
    {
        public int StoreId { get; set; }
        public string SiteName { get; set; }
        public string Url { get; set; }
        public int? SocialMediaLogoId { get; set; }

        public int DisplayOrder { get; set; }
        /// <summary>
        /// Font awesome class for logo, eg fb : fab fa-facebook-f
        /// </summary>
        public string SocialMediaLogoClass { get; set; }

        public virtual File SocialMediaLogo { get; set; }

        public virtual Store Store { get; set; }

    }
}
