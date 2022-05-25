using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.Files;
using System.Collections.Generic;

namespace PreSchool.Data.Entities.Stores
{
    /// <summary>
    /// Represents a store
    /// </summary>
    public partial class Store : CommonProperties
    {

        /// <summary>
        /// Gets or sets the company name
        /// </summary>
        public string Name { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the store URL
        /// </summary>
        public string Url { get; set; }


        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }



        /// <summary>
        /// Gets or sets the store phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        public string PAN { get; set; }



        /// <summary>
        /// All the user of the store include all type of user such as customer, vendor, store user
        /// </summary>
        public virtual ICollection<AppUser> AppUsers { get; set; }

        public virtual ICollection<StoreFeature> StoreFeatures { get; set; }


        public virtual ICollection<StoreUser> StoreUsers { get; set; }


        public virtual ICollection<StoreMapping> StoreMappings { get; set; }


        public virtual ICollection<StoreAddress> StoreAddresss { get; set; }



        public virtual ICollection<StoreSocialMedia> StoreSocialMedias { get; set; }


        public virtual ICollection<StoreCollectionCenter> StoreCollectionCenters { get; set; }



        public virtual ICollection<StoreImage> StoreImages { get; set; }



        public Store()
        {

            StoreImages = new HashSet<StoreImage>();
            StoreCollectionCenters = new HashSet<StoreCollectionCenter>();
            StoreSocialMedias = new HashSet<StoreSocialMedia>();
            StoreAddresss = new HashSet<StoreAddress>();
            StoreMappings = new HashSet<StoreMapping>();
            AppUsers = new HashSet<AppUser>();
            StoreFeatures = new HashSet<StoreFeature>();
            StoreUsers = new HashSet<StoreUser>();
        }







    }
}
