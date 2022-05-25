using PreSchool.Application.Infastructures;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchool.Application.Events
{
    /// <summary>
    /// Represents the event publisher implementation
    /// </summary>
    public partial class EventPublisher : IEventPublisher
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public EventPublisher(
            ICurrentUserService currentUserService,
            IDateTime dateTime)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }



        #region Methods

        /// <summary>
        /// Publish event to consumers
        /// </summary>
        /// <typeparam name="TEvent">Type of event</typeparam>
        /// <param name="event">Event object</param>
        public virtual void Publish<TEvent>(TEvent @event, object parameter = null)
        {
            //get all event consumers
            var consumers = ResolveAll<IEventConsumer<TEvent>>().ToList();

            foreach (var consumer in consumers)
            {
                var sender = new EventSender()
                {
                    EventDateTime = _dateTime.Now,
                };

                if (_currentUserService != null && _currentUserService.IsAuthenticated)
                {
                    sender.AppUserId = _currentUserService.AppUserId;
                }
                try
                {

                    //try to handle published event
                    consumer.HandleEvent(@event, sender, parameter);
                }
                catch (Exception exception)
                {
                    Logger.Error(exception, sender, parameter);
                }
            }
        }


        protected IServiceProvider GetServiceProvider()
        {
            var context = _currentUserService?.HttpContext;
            return context?.RequestServices;
        }

        public virtual IEnumerable<T> ResolveAll<T>()
        {
            return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
        }
        #endregion
    }
}