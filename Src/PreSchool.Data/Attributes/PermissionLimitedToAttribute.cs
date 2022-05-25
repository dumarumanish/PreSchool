using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Attributes
{
    public class PermissionLimitedToAttribute : Attribute
    {
        public AppUserTypeEnum[] LimitedToAppUserType { get; set; }
        public AppFeaturesEnum? LimitedToFeature { get; set; }

        /// <summary>
        /// Limited to appuser type
        /// </summary>
        /// <param name="userType"></param>
        public PermissionLimitedToAttribute(params AppUserTypeEnum[] userType )
        {
            LimitedToAppUserType = userType;
        }

        /// <summary>
        /// Limited to the feature, if the store have that particular feature then 
        /// </summary>
        /// <param name="userType"></param>
        public PermissionLimitedToAttribute(AppFeaturesEnum feature)
        {
            LimitedToFeature = feature;
        }


    }
}
