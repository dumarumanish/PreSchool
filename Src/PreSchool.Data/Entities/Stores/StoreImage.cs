using PreSchool.Data.Entities.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Stores
{
    public class StoreImage : CommonProperties
    {

        public int StoreId { get; set; }

        public int FileId { get; set; }

        public int StoreImageTypeId { get; set; }

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

        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual Store Store { get; set; }


        public virtual StoreImageType StoreImageType { get; set; }
        public virtual File File { get; set; }


    }
}
