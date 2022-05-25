using PreSchool.Application.Services.Addresses.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.FeeSettings.Models.Dtos
{
    public class MonthDto
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

        public int SortOrder { get; set; }

    }
}
