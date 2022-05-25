using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Contacts
{
    public class IssueType : CommonProperties
    {
        
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public string Description { get; set; }
    }
}
