using PreSchool.Application.Services.Addresses.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.FeeSettings.Models.Commands
{
    public class InsertUpdateFeeRate
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int GradeId { get; set; }
        public int FeeTitleId { get; set; }
        public decimal Amount { get; set; }
        public string UniqueId { get; set; }
        public string Remark { get; set; }


    }
}
