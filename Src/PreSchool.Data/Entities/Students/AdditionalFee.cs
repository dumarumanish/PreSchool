using PreSchool.Data.Entities.Files;
using PreSchool.Data.Entities.Schools;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Students
{
    public class AdditionalFee : CommonProperties
    {
 
        public int BatchId  { get; set; }
        public int GradeId  { get; set; }
        public string Section  { get; set; }
        public int StudentId  { get; set; }
        public int RollNo { get; set; }
        public DateTime BillingPeriod { get; set; }
      
        public virtual Batch Batch { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual StudentRegistration Student { get; set; }

    }
}
