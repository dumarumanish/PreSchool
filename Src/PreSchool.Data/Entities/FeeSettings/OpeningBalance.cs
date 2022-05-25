using PreSchool.Data.Entities.Schools;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.FeeSettings
{
    public class OpeningBalance : CommonProperties
    {
        public int BatchId { get; set; }
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public string Remark { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public decimal DueAmount { get; set; }
        public decimal AdvanceAmount { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual StudentRegistration Student { get; set; }


    }
}
