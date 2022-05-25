using PreSchool.Application.Services.Addresses.Models.Commands;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Students.Models.Commands
{
    public class InsertUpdateAdditionalFee
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
