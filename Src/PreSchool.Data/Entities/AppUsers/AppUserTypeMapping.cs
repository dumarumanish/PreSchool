using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.AppUsers
{
   
        /// <summary>
        /// Mapping of entity record to perticular app user type
        /// Entity may be any thing like Product, Category, Roles, Blogs, etc
        /// Example: 
        /// EntityName : Product
        /// EntityId : 1, Id of the product
        /// AppUserTypeId : 1, Id of the appuser type on which product belongs
        /// </summary>
        public class AppUserTypeMapping : CommonProperties
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
            /// Gets or sets the app user type identifier
            /// </summary>
            public int AppUserTypeId { get; set; }

            public virtual AppUserType AppUserType { get; set; }

        }
    
}
