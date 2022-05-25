using PreSchool.Application.Services.Addresses.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.FeeSettings.Models.Commands
{
    public class InsertUpdateOpeningBalance
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public string Remark { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public decimal DueAmount { get; set; }
        public decimal AdvanceAmount { get; set; }


    }
}
