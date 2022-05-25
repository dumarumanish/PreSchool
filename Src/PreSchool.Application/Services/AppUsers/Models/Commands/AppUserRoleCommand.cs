using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Commands
{
    public class AppUserRoleCommand
    {
        public int AppUserId { get; set; }
        public int RoleId { get; set; }
       // public bool IsDeleted { get; set; }
    }
}
