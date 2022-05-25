namespace PreSchool.Data.Entities.AppUsers
{
    /// <summary>
    /// AppUser activated event
    /// </summary>
    public class AppUserActivatedEvent
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="appUser">appUser</param>
        public AppUserActivatedEvent(AppUser appUser)
        {
            AppUser = appUser;
        }

        /// <summary>
        /// AppUser
        /// </summary>
        public AppUser AppUser
        {
            get;
        }
    }
}
