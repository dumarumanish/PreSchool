using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Stores
{
    public class StoreAddress : CommonProperties
    {
        public int StoreId { get; set; }
        public int AddressId { get; set; }
        public int DisplayOrder { get; set; }
        public virtual Store Store { get; set; }

        public virtual Address Address { get; set; }

    }

}
