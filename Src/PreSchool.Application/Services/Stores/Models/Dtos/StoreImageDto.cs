using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Stores.Models.Dtos
{
    public class StoreImageDto
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public int StoreImageTypeId { get; set; }
        public string StoreImageType { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Title of image
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Heading of the image, eg: slider heading
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Sub heading of the image, eg: slider sub heading
        /// </summary>
        public string SubHeading { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// Redirect link on clicking the image
        /// </summary>
        public string RedirectLink { get; set; }
        public string ImageLocation { get; set; }
        public int DisplayOrder { get; set; }

    }
}
