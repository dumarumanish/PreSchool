using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Addresses.Models.Filters
{
    public class AddressFilter
    {
       
    
        public bool? AllowsBilling { get; set; }

        public bool? AllowsShipping { get; set; }
     
        public bool? SubjectToVat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool? Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool? LimitedToStores { get; set; }
    }
}
