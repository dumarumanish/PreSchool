using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.AppUsers
{
    public class AppUserOtpCode : BaseEntity
    {
        public int  AppUserId { get; set; }
        public string Code { get; set; }
        public DateTime CodeGeneratedOn { get; set; }
        public DateTime CodeExpireOn { get; set; }
        public bool IsCodeUsed { get; set; }
        public DateTime? CodeUsedOn { get; set; }
    }
}
