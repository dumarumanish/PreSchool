using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Common.Addresses
{
    public class City : CommonProperties
    {
        public int ProvinceId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        public virtual Province Province { get; set; }

        public virtual ICollection<Region> Regions { get; set; }


        public City()
        {
            Regions = new HashSet<Region>();

        }
    } 
}
