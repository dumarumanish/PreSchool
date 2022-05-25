using PreSchool.Application.Events;
using PreSchool.Data.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Settings
{
    public class SettingEventConsumer : IEventConsumer<SeoSettingChangedEvent>
    {
        

        public void HandleEvent(SeoSettingChangedEvent eventMessage, EventSender sender, object parameter = null)
        {
            throw new NotImplementedException();
        }
    }
}
