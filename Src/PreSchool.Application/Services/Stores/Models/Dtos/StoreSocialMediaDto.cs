using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Stores.Models.Dtos
{
    public class StoreSocialMediaDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string SiteName { get; set; }
        public string Url { get; set; }
        public int? SocialMediaLogoId { get; set; }
        public string SocialMediaLogo { get; set; }
        public string SocialMediaLogoClass { get; set; }

        public int DisplayOrder { get; set; }

    }
}
