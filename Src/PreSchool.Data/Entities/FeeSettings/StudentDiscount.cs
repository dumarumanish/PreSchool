using PreSchool.Data.Entities.Schools;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.FeeSettings
{
    public class StudentDiscount : CommonProperties
    {
        public int DiscountId { get; set; }
        public string Remark { get; set; }

        public virtual Discount Discount { get; set; }


    }
}
