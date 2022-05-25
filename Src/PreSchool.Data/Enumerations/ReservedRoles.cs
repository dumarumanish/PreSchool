using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Enumerations
{
    public enum ReservedRoles
    {
        [Display(Name = "Super Admin", Description = "Super admin with total access permission")]
        SuperAdmin = 1,
        InternalAdmin,
        Customer
    }
}
