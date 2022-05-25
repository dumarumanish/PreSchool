using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Students.Models.Queries
{
    public class StudentEnquiryFilter : PaginationFilter
    {
        public GenderEnum? GenderId { get; set; }

    }
}
