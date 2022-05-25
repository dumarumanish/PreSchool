using PreSchool.Data.Entities.Taxes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Taxes
{
    public class TaxSettingChangedEvent
    {
        public TaxSettingChangedEvent(TaxSetting seoSetting)
        {
            TaxSetting = seoSetting;
        }
        public TaxSetting TaxSetting { get; }
    }
}
