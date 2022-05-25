using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Stores
{
    /// <summary>
    /// Mapping of entity record to perticular store
    /// Entity may be any thing like Product, Category, Roles, Blogs, etc
    /// Example: 
    /// EntityName : Product
    /// EntityId : 1, Id of the product
    /// StoreId : 1, Id of the store on which product belongs
    /// </summary>
    public class StoreMapping :CommonProperties
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Gets or sets the entity name
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets the store identifier
        /// </summary>
        public int StoreId { get; set; }

        public virtual Store Store { get; set; }

    }
}
