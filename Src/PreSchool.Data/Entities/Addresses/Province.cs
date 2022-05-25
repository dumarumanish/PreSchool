using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Common.Addresses
{
    public class Province: CommonProperties
    {
        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public Province()
        {
            Cities = new HashSet<City>();

        }
    }
}
