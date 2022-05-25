using PreSchool.Application.Services.Addresses.Models.Commands;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Students.Models.Commands
{
    public class InsertUpdateBadDept
    {
        public int Id { get; set; }
        public string Date { get; set; }
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
    }
}
