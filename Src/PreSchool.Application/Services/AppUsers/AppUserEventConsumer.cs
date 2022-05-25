using PreSchool.Application.Events;
using PreSchool.Data.Entities;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppUsers
{
    public class AppUserEventConsumer : IEventConsumer<AppUserLoggedinEvent>, IEventConsumer<AppUserRegisteredEvent>
    {
      
        public void HandleEvent(AppUserRegisteredEvent eventMessage, EventSender sender, object parameter = null)
        {
            
        }

        public void HandleEvent(AppUserLoggedinEvent eventMessage, EventSender sender, object parameter = null)
        {
        }
    }

    public class AppUsertestEventConsumer : IEventConsumer<AppUserLoggedOutEvent>
    {
        
        public void HandleEvent(AppUserLoggedOutEvent eventMessage, EventSender sender, object parameter = null)
        {
        }
    }

    public class CustomerEvent<TEntity> : IEventConsumer<EntityInsertedEvent<TEntity>>,
        IEventConsumer<EntityUpdatedEvent<TEntity>>,
        IEventConsumer<EntityDeletedEvent<TEntity>> where TEntity : BaseEntity
    {
      
        public void HandleEvent(EntityInsertedEvent<TEntity> eventMessage, EventSender sender, object parameter = null)
        {
            throw new NotImplementedException();
        }

        public void HandleEvent(EntityUpdatedEvent<TEntity> eventMessage, EventSender sender, object parameter = null)
        {
            throw new NotImplementedException();
        }

        public void HandleEvent(EntityDeletedEvent<TEntity> eventMessage, EventSender sender, object parameter = null)
        {
            throw new NotImplementedException();
        }
    }
}
