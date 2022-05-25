using PreSchool.Application.Services.Addresses.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.FeeSettings.Models.Commands
{
    public class InsertUpdateDiscount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public decimal Percentage { get; set; }


    }
}
