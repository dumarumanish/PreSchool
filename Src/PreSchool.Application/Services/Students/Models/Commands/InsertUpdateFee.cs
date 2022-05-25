using PreSchool.Application.Services.Addresses.Models.Commands;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Students.Models.Commands
{
    public class InsertUpdateFee
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int GradeId { get; set; }
        public string Section { get; set; }
        public int StudentId { get; set; }
        public int RollNo { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Remark { get; set; }
        public BillingTypeEnum BillingTypeId { get; set; }
    }
}
