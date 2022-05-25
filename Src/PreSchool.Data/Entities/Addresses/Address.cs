using PreSchool.Data.Entities.Common.Addresses;
using System;

namespace PreSchool.Data.Entities
{
    /// <summary>
    /// Address
    /// </summary>
    public partial class Address : CommonProperties
    {
        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the state/province identifier
        /// </summary>
        public int? CountryProvinceId { get; set; }
   

        public int? CityId { get; set; }
        public int? RegionId { get; set; }
        public virtual City City { get; set; }
        public virtual Region Region { get; set; }

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
        /// Gets or sets the Secondary phone number
        /// </summary>
        public string SecondaryPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        public string FaxNumber { get; set; }
        /// <summary>
        /// direction on link of map of the define location.
        /// </summary>
        public string MapLink { get; set; }


        public virtual Country Country { get; set; }
        public virtual Province CountryProvince { get; set; }


    }
}
