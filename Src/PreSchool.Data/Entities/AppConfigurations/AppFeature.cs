using PreSchool.Data.Entities.Stores;
using System.Collections.Generic;

namespace PreSchool.Data.Entities.AppConfigurations
{
    /// <summary>
    /// Permission of the application
    /// </summary>
    public class AppFeature 
    {
        //  Id
        public int Id { get; set; }

        public int AppFeatureGroupId { get; set; }

        // Name of the AppFeature 
        public string Name { get; set; }

        // Description of the feature  discription
        public string Description { get; set; }


        public virtual AppFeatureGroup AppFeatureGroup { get; set; }

        public virtual ICollection<StoreFeature> StoreFeatures { get; set; }

    }
}
