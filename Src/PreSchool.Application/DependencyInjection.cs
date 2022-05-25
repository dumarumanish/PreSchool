using PreSchool.Application.Events;
using PreSchool.Application.Services.AppUsers;
using PreSchool.Application.Services.Notifications;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace PreSchool.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            var allServices = typeof(IAppUserService).Assembly.DefinedTypes;
            foreach (var intfc in allServices.Where(t => t.IsInterface))
            {
                var impl = allServices.FirstOrDefault(c => c.IsClass && intfc.Name.Substring(1) == c.Name);
                if (impl != null)
                    services.AddScoped(intfc, impl);
            }

            //event consumers
            //services.AddScoped<IConsumer<AppUserLoggedinEvent>, AppUserEventConsumer>();
            //services.AddScoped<IConsumer<AppUserRegisteredEvent>, AppUserEventConsumer>();

            Assembly.GetExecutingAssembly()
                         .GetTypes()
                         .Where(item => item.GetInterfaces()
                         .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(IEventConsumer<>)) && !item.IsAbstract && !item.IsInterface)
                         .ToList().ForEach(impType =>
                         {
                             impType.GetInterfaces().Where(i => i.GetGenericTypeDefinition() == typeof(IEventConsumer<>)).ToList()
                             .ForEach(serviceType =>
                             {
                                 if (!serviceType.ContainsGenericParameters)
                                     services.AddScoped(serviceType, impType);
                                 else
                                 {
                                     // Register for nested implementation
                                     //public class CustomerEvent<TEntity> : IEventConsumer<EntityInsertedEvent<TEntity>>, IEventConsumer<EntityDeletedEvent<TEntity>> where TEntity : BaseEntity

                                 }
                             });
                         });

            services.AddTransient<IEventConsumer<EntityInsertedEvent<AppUser>>, CustomerEvent<AppUser>>();
          

            return services;
        }


        #region Signal R Notification.
        public static IServiceCollection AddSignalRNotification(this IServiceCollection services)
        {
            // Add signalR
            services.AddSignalR();
            services.AddSingleton<IConnectionManager, ConnectionManager>();
            services.AddSingleton<IHubNotificationHelper, HubNotificationHelper>();
            return services;
        }
        #endregion 

    }
}
