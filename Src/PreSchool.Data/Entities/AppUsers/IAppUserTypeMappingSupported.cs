

namespace PreSchool.Data.Entities.AppUsers

{
    /// <summary>
    /// Represents an entity which supports app user mapping
    /// </summary>
    public partial interface IAppUserTypeMappingSupported
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain app users
        /// </summary>
        bool LimitedToAppUsers{ get; set; }
    
    }
}
