using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Addresses.Models.Commands
{
    public class InsertUpdateCountry
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
         
        /// <summary>
        /// Whether billing is allowed to this country
        /// </summary>
        [Required (ErrorMessage ="Allows Billing is required")]
        public bool AllowsBilling { get; set; }

        /// <summary>
        /// Whether shipping is allowed to this country
        /// </summary>
        [Required(ErrorMessage = "Allows Shipping is required")]
        public bool AllowsShipping { get; set; }

        /// <summary>
        /// The two letter ISO code
        /// </summary>
        public string TwoLetterIsoCode { get; set; }

        /// <summary>
        /// The three letter ISO code
        /// </summary>
        public string ThreeLetterIsoCode { get; set; }

        /// <summary>
        /// The numeric ISO code
        /// </summary>
        public int NumericIsoCode { get; set; }

        /// <summary>
        /// Whether customers in this country must be charged EU VAT
        /// </summary>
        public bool SubjectToVat { get; set; }

        /// <summary>
        /// Whether the address is published
        /// </summary>
        [Required(ErrorMessage = "Whether the address is published is required")]
        public bool Published { get; set; }

        /// <summary>
        /// The display order
        /// </summary>
        public int DisplayOrder { get; set; }

       public LimitedToStoresCommand LimitedToStores { get; set; }

    }
}
