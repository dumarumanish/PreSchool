using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Stores
{
    public class StoreCollectionCenter : CommonProperties
    {

        public int StoreId { get; set; }

        public int WarehouseId { get; set; }


        public string Name { get; set; }
        public int AddressId { get; set; }
        public string Email { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhoneNumber { get; set; }
        public int DisplayOrder { get; set; }
        public virtual Store Store { get; set; }
        public virtual Address Address { get; set; }


    }
}
