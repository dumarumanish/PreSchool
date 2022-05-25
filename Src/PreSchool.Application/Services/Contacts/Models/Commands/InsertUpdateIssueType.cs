using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Contacts.Models.Commands
{
    public class InsertUpdateIssueType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }
}
