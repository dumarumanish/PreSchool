using Microsoft.AspNetCore.Http;
using PreSchool.Data.Entities.Common.Addresses;
using PreSchool.Data.Entities.Departments;
using PreSchool.Data.Entities.Files;
using PreSchool.Data.Entities.Schools;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Commands
{
    public class InsertUpdateInternalUserDocuments
    {
        public int appUserId { get; set; }
        public int id { get; set; }
        public string documentType { get; set; }
        public string documentName { get; set; }
        public string documentNumber { get; set; }
        public string documentIssuedFrom { get; set; }
        public string documentIssuedOn { get; set; }
        public IFormFile image { get; set; }



    }
}
