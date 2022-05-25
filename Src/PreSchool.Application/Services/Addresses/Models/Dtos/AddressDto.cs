using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Addresses.Models.Dtos
{
    public class AddressDto
    {
        /// <summary>
        /// Id of the address if update
        /// </summary>
        public int Id { get; set; }
   
        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int? CountryId { get; set; }

        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the state/province identifier
        /// </summary>
        public int? CountryProvinceId { get; set; }

        public string CountryProvince { get; set; }

        public int? CityId { get; set; }
        public string City { get; set; }
        public int? RegionId { get; set; }
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the address 1
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the zip/postal code
        /// </summary>
        public string ZipPostalCode { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public string SecondaryPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        public string FaxNumber { get; set; }
        public string MapLink { get; set; }

    }
}
