using Microsoft.AspNetCore.Http;
using PreSchool.Application.Services.Addresses.Models.Commands;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Students.Models.Commands
{
    public class InsertStudentEnquiryImage
    {
        public int id { get; set; }
        public int studentEnquiryId { get; set; }
        public IFormFile image { get; set; }
    }
}
