using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.AppUsers
{
   public  class AppUserLoginHistory
    {

        public int Id { get; set; }
        public int AppUserId { get; set; }
        public DateTime LoginDateTime { get; set; }

        /// <summary>
        /// Gets or sets the IP address
        /// </summary>
        public string IpAddress { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
