using PreSchool.Application.Services.Addresses.Models.Commands;
using PreSchool.Data.Entities.FeeSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.FeeSettings.Models.Commands
{
    public class InsertUpdateFeeTitle
    {
        public int Id { get; set; }
        public int FeeGroupId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string UniqueId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDiscountApplicable { get; set; }
        public bool IsTaxable { get; set; }
        public FeeTypeEnum FeeTypeId { get; set; }



    }
}
