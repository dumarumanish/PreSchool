using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications.Models.Dtos
{
    public class RadiusCustomerSyncNotificationDto
    {
        public int IspId { get; set; }
        public DateTime SyncDateTime { get; set; }
        public int Synced { get; set; }
        public int Total { get; set; }
        public bool SyncFailed { get; set; }
        public string Remark { get; set; }
    }
}
