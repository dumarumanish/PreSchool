using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Enumerations
{
    public enum PermissionGroups
    {
        //[Display(Description = "Super admin of the application")]
        //SuperAdmin = 1,

        [Display(Description = "Internal app management ")]
        InternalAppManagement = 1,

        [Display(Description = "Setting management")]
        SettingManagement,

        [Display(Description = "Role management")]
        RoleManagement ,

        [Display(Description = "App feature management")]
        AppFeatureManagement ,

        [Display(Description = "Address management")]
        AddressManagement,

        [Display(Description = "Department management")]
        DepartmentManagement ,
      
        [Display(Description = "Tax management")]
        TaxManagement,

        [Display(Description = "Payment management")]
        PaymentManagement,

        [Display(Description = "Ticket management")]
        TicketManagement,

        [Display(Description = "Contact management")]
        ContactManagement,

        [Display(Description = "Log management")]
        LogManagement,

        [Display(Description = "Notification management")]
        NotificationManagement,
    }
}
