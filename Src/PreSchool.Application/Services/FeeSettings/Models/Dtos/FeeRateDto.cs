using PreSchool.Application.Services.Addresses.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.FeeSettings.Models.Dtos
{
    public class FeeRateDto
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
