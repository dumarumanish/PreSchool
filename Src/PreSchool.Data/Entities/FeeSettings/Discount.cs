using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.FeeSettings
{
    public class Discount : CommonProperties
    {
        public string Name { get; set; }
        public string Remark { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public decimal Percentage { get; set; }


    }
}
