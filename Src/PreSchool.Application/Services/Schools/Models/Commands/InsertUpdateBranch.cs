using PreSchool.Application.Services.Addresses.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Schools.Models.Commands
{
    public class InsertUpdateBranch
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

        /// <summary>
        /// Gets or sets the address identifier of the school
        /// </summary>
        public int AddressId { get; set; }
        public InsertUpdateAddress Address { get; set; }


    }
}
