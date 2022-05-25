using PreSchool.Data.Entities.AppUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Stores
{
    public class StoreUser : CommonProperties
    {
        public int StoreId { get; set; }
        public int AppUserId { get; set; }



        public virtual Store Store { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
