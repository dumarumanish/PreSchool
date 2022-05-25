using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Settings.Models.Command
{
    public class InsertUpdateBank
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string SwiftCode { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }
}
