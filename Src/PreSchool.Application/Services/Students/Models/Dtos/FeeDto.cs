using PreSchool.Application.Services.Addresses.Models.Dtos;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Students.Models.Dtos
{
    public class FeeDto
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
