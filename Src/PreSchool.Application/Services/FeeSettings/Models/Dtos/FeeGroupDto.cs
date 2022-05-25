using PreSchool.Application.Services.Addresses.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.FeeSettings.Models.Dtos
{
    public class FeeGroupDto
    {
        public int Id { get; set; }
        public int BillPeriodId { get; set; }
        public string BillPeriodName { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string UniqueCode { get; set; }
        public bool IsActive { get; set; }

    }
}
