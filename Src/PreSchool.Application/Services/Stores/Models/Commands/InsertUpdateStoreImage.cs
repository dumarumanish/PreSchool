using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace PreSchool.Application.Services.Stores.Models.Commands
{
    public class InsertUpdateStoreImage
    {
        public int id { get; set; }

        public int storeId { get; set; }

        public int storeImageTypeId { get; set; }
        [Required]
        public string name { get; set; }

        /// <summary>
        /// Title of image
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Heading of the image, eg: slider heading
        /// </summary>
        public string heading { get; set; }

        /// <summary>
        /// Sub heading of the image, eg: slider sub heading
        /// </summary>
        public string subHeading { get; set; }
        public string description { get; set; }

        /// <summary>
        /// Redirect link on clicking the image
        /// </summary>
        public string redirectLink { get; set; }

        public int displayOrder { get; set; }
        [Required]
        public IFormFile image { get; set; }
        public bool IsActive { get; set; }
    }
}
