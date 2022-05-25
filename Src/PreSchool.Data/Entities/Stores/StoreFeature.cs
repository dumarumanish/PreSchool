using PreSchool.Data.Entities.AppConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Stores
{
    public class StoreFeature : CommonProperties
    {
        public int StoreId { get; set; }
        public int AppFeatureId { get; set; }

        public virtual Store Store { get; set; }


        public virtual AppFeature AppFeature { get; set; }

    }
}
