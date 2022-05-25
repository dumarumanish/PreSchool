namespace PreSchool.Data.Entities.AppUsers
{
    /// <summary>
    /// "AppUser is logged out" event
    /// </summary>
    public class AppUserLoggedOutEvent
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="user">AppUser</param>
        public AppUserLoggedOutEvent(AppUser user)
        {
            AppUser = user;
        }

        /// <summary>
        /// Get or set the user
        /// </summary>
        public AppUser AppUser { get; }
    }
}