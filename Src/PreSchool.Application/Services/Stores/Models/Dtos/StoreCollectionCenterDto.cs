using PreSchool.Application.Services.Addresses.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Stores.Models.Dtos
{
    public class StoreCollectionCenterDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
        public string Email { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhoneNumber { get; set; }
        public int DisplayOrder { get; set; }
    }
}
