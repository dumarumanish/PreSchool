using PreSchool.Data.Entities.Schools;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.FeeSettings
{
    public class FeeRate : CommonProperties
    {
        public int BatchId { get; set; }
        public int GradeId { get; set; }
        public int FeeTitleId { get; set; }
        public decimal Amount { get; set; }
        public string UniqueId { get; set; }
        public string Remark { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual FeeTitle FeeTitle { get; set; }


    }
}
