using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Addresses.Models.Commands
{
    public class InsertUpdateRegion
    {
        public int Id { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
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
