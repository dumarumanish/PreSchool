using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Students.Models.Queries
{
    public class StudentRegistrationFilter : PaginationFilter
    {
        public bool? EnquiredStudent { get; set; }

    }
}
