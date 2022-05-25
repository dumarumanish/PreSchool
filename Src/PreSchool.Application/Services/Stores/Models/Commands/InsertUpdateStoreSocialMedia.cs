using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PreSchool.Application.Services.Stores.Models.Commands
{
    public class InsertUpdateStoreSocialMedia
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        [Required]
        public string SiteName { get; set; }
        [Required]
        public string Url { get; set; }
        public int DisplayOrder { get; set; }
        public IFormFile SiteLogo { get; set; }
        /// <summary>
        /// Font awesome class for logo, eg fb : fab fa-facebook-f
        /// </summary>
        public string SocialMediaLogoClass { get; set; }

    }
}
