using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.AppUsers.Events
{
    public class AppUserUpdatedEvent
    {
        public AppUserUpdatedEvent(AppUser appUser)
        {
            AppUser = appUser;
        }

        public AppUser AppUser { get; }
    }
}
