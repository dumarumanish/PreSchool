using PreSchool.Data.Entities.Seo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Settings
{
    public class SeoSettingChangedEvent
    {
        public SeoSettingChangedEvent(SeoSetting seoSetting)
        {
            SeoSetting = seoSetting;
        }
        public SeoSetting SeoSetting { get;  }
    }
}
