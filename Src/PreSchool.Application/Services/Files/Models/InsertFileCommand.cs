using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Files.Models
{
    public class InsertFileCommand
    {
        public IFormFile File { get; set; }

        /// <summary>
        /// Name of the entity of which this file is corresponded
        /// eg: Product, Customer
        /// </summary>
        public string EntityName { get; set; }
    }

}
