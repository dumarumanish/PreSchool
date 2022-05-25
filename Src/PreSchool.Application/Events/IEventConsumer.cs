using System.Threading.Tasks;

namespace PreSchool.Application.Events
{
    /// <summary>
    /// Consumer interface
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public interface IEventConsumer<T>
    {
        /// <summary>
        /// Handle event
        /// </summary>
        /// <param name="eventMessage">Event</param>
        void HandleEvent(T eventMessage, EventSender sender, object parameter = null);
    }
}
