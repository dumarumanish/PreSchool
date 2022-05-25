using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.AppUsers
{
    public class AppUserRole : CommonProperties
    {
        public int AppUserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
