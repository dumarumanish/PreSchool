using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Settings.Models.Dtos
{
    public class BankDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SwiftCode { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }
}
