using PreSchool.Data.Entities.Schools;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.FeeSettings
{
    public class StudentFee : CommonProperties
    {
        public int BatchId { get; set; }
        public int GradeId { get; set; }
        public int FeeRateId { get; set; }
        public int StudentId { get; set; }
        public string Remark { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual FeeRate FeeRate { get; set; }
        public virtual StudentRegistration Student { get; set; }


    }
}
