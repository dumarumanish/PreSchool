using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Addresses.Models.Dtos
{
    public class RegionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
