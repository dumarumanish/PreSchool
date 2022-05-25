namespace PreSchool.Data.Entities.AppUsers
{
    /// <summary>
    /// AppUser logged-in event
    /// </summary>
    public class AppUserLoggedinEvent
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="user">AppUser</param>
        public AppUserLoggedinEvent(AppUser user)
        {
            AppUser = user;
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