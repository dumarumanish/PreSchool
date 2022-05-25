using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Commands
{
    public class InsertUpdateVendorDepartment
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int DisplayOrder { get; set; }
    }
}
