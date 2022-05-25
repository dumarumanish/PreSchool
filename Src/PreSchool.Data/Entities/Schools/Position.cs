using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Schools
{
    public class Position : CommonProperties
    {
        /// <summary>
        /// Gets or sets the school position
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }


    }
}
