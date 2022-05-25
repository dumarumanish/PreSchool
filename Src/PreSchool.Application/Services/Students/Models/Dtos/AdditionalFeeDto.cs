using PreSchool.Application.Services.Addresses.Models.Dtos;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Students.Models.Dtos
{
    public class AdditionalFeeDto
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int GradeId { get; set; }
        public string Section { get; set; }
        public int StudentId { get; set; }
        public int RollNo { get; set; }
        public DateTime BillingPeriod { get; set; }


    }
}
