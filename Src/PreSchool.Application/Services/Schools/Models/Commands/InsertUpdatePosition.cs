using PreSchool.Application.Services.Addresses.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Schools.Models.Commands
{
    public class InsertUpdatePosition
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }



    }
}
