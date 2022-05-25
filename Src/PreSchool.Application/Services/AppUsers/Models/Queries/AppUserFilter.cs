using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Queries
{
    public class AppUserFilter
    {
        public int? StoreId { get; set; }
        public int? RoleId { get; set; }
        public int? UserTypeId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public string Search { get; set; }
        public bool? IsActive { get; set; }
    }
}
