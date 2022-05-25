using PreSchool.Application.Services.Addresses.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Stores.Models.Dtos
{
    public class StoreAddressDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public AddressDto  Address { get; set; }
        public int DisplayOrder { get; set; }
    }
}
