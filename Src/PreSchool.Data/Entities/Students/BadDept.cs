using PreSchool.Data.Entities.Files;
using PreSchool.Data.Entities.Schools;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Students
{
    public class BadDept : CommonProperties
    {
 
        public string Date  { get; set; }
        public string BadDeptNo { get; set; }
        public int BatchId { get; set; }
        public int GradeId { get; set; }
        public string Section { get; set; }
        public int RollNo { get; set; }
        public int StudentId { get; set; }
        public decimal Amount { get; set; }
        public decimal Due { get; set; }
        public string Remark { get; set; }
        public int EnteredBy { get; set; }
        public int? ApprovedBy { get; set; }
        public BadDeptTypeEnum BadDeptTypeId { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual StudentRegistration Student { get; set; }

    }
}
