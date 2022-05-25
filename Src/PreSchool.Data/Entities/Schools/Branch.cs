using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Schools
{
    public class Branch : CommonProperties
    {
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

        public virtual Address Address { get; set; }

    }
}
