using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Common.Addresses
{
    public class Region : CommonProperties
    {
        public int CityId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        public virtual City City { get; set; }

        
    }
}
