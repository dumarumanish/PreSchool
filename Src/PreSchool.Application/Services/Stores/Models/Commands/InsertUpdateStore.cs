using PreSchool.Application.Services.Addresses.Models.Commands;
using PreSchool.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Stores.Models.Commands
{
    public class InsertUpdateStore
    { 
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the company name
        /// </summary>
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the store URL
        /// </summary>
        public string Url { get; set; }


        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }


        /// <summary>
        /// Gets or sets the company address
        /// </summary>
        /// 
        public List<InsertUpdateStoreAddress> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the store phone number
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        public string PAN { get; set; }

     
    }
}
