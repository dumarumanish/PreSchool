using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Contacts.Models.Dtos
{
    public class IssueTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public string Description { get; set; }
    }
}
