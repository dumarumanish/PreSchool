using PreSchool.Application.Services.Addresses.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.FeeSettings.Models.Dtos
{
    public class DiscountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public decimal Percentage { get; set; }
    }
}
