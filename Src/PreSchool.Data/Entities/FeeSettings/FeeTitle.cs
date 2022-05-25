using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.FeeSettings
{
    public class FeeTitle : CommonProperties
    {
        public int FeeGroupId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string UniqueId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDiscountApplicable { get; set; }
        public bool IsTaxable { get; set; }
        public FeeTypeEnum FeeTypeId { get; set; }
        public virtual FeeGroup FeeGroup { get; set; }


    }
}
