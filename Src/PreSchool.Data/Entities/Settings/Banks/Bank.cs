using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Settings
{
    public class Bank : CommonProperties
    {
        public string Name { get; set; }
        public string SwiftCode { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }


    }
}
