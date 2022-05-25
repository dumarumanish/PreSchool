using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Addresses.Models.Filters
{
    public class DepartmentFilter
    {
        public int? AppUserTypeId { get; set; }
        public int? StoreId { get; set; }
        public int? VendorId { get; set; }
    }
}
