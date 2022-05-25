using PreSchool.Application.Services.Addresses.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Schools.Models.Dtos
{
    public class BatchDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the school name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }

       
    }
}
