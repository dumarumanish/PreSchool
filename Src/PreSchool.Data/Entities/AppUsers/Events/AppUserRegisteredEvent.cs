namespace PreSchool.Data.Entities.AppUsers
{
    /// <summary>
    /// AppUser registered event
    /// </summary>
    public class AppUserRegisteredEvent
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="user">user</param>
        public AppUserRegisteredEvent(AppUser user)
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