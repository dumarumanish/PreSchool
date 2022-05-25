namespace PreSchool.Data.Entities.AppUsers
{
    /// <summary>
    /// AppUser password changed event
    /// </summary>
    public class AppUserPasswordChangedEvent
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="password">Password</param>
        public AppUserPasswordChangedEvent(AppUserPassword password)
        {
            Password = password;
        }

        /// <summary>
        /// AppUser password
        /// </summary>
        public AppUserPassword Password { get; }
    }
}