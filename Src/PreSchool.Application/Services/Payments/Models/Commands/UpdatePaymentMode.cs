using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Payments.Models.Commands
{
    public class UpdatePaymentMode
    {
        public int id { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public int displayOrder { get; set; }
        public bool isActive { get; set; }
        public IFormFile    image { get; set; }

    }
}
