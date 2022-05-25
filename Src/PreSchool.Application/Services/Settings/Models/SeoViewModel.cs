using PreSchool.Data.Entities.Seo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Settings.Models
{
    public class SeoViewModel
    {
        public List<EnumValue> PageTitleSeoAdjustmentEnums { get;  }
        public List<EnumValue> WwwRequirementEnums { get;  }

        public SeoViewModel()
        {
            PageTitleSeoAdjustmentEnums = EnumHelper.GetEnumValues<PageTitleSeoAdjustment>();
            WwwRequirementEnums = EnumHelper.GetEnumValues<WwwRequirement>();
        }
    }
}
