using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Stores.Models.Dtos
{
    public class StoreDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }

        public int DisplayOrder { get; set; }

       
        public string PhoneNumber { get; set; }
        public string PAN { get; set; }

        public  List<StoreAddressDto> StoreAddresses { get; set; }
        public  List<StoreSocialMediaDto> StoreSocialMedias { get; set; }
        public  List<StoreImageDto> StoreImages { get; set; }


    }
}
