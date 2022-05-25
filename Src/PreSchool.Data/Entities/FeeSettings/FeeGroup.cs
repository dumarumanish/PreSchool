using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.FeeSettings
{
    public class FeeGroup : CommonProperties
    {
        public int BillPeriodId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string UniqueCode { get; set; }
        public bool IsActive { get; set; }
        public virtual BillPeriod BillPeriod { get; set; }


    }
}
