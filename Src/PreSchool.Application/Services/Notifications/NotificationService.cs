using PreSchool.Application.Events;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Services.AppConfigurations;
using PreSchool.Application.Services.AppUsers.Models.Dtos;
using PreSchool.Application.Services.BackgroundTasks;
using PreSchool.Application.Services.Notifications.Models.Commands;
using PreSchool.Application.Services.Notifications.Models.Dtos;
using PreSchool.Application.Services.Notifications.Models.Queries;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.Notifications;
using PreSchool.Data.Entities.Notifications.Events;
using PreSchool.Data.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IAppFeatureService _featureService;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IHubNotificationHelper _hubNotificationHelper;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IEventPublisher _eventPublisher;
        private readonly IBackgroundTaskService _backgroundTaskService;

        public NotificationService(
            IAppFeatureService featureService,
            IApplicationDbContext context,
            ICurrentUserService currentUserService,
            IDateTime dateTime,
            IHubNotificationHelper hubNotificationHelper,
             IServiceScopeFactory scopeFactory,
            IEventPublisher eventPublisher,
            IBackgroundTaskService backgroundTaskService
            )
        {
            _featureService = featureService;
            _context = context;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _hubNotificationHelper = hubNotificationHelper;
            _scopeFactory = scopeFactory;
            _eventPublisher = eventPublisher;
            _backgroundTaskService = backgroundTaskService;
        }

        #region Notification Activity Type

        public Task<List<NotificationActivityTypeDto>> GetNotificationAcitivityTypes()
        {
            return _context.NotificationActivityTypes
                .Select(t => new NotificationActivityTypeDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    RedirectUrl = t.RedirectUrl,
                    AdminRedirectUrl = t.AdminRedirectUrl,
                    Description = t.Description,
                    DisplayName = t.DisplayName,
                    GroupName = t.GroupName,
                }).ToListAsync();
        }

        public async Task<bool> UpdateNotificationActivityType(UpdateNotificationActivityType activityType)
        {
            var notificationType = await _context.NotificationActivityTypes
                .FirstOrDefaultAsync(n => n.Id == activityType.Id);

            if (notificationType == null)
                throw new BadRequestException("Invalid notificaiton activity type");
            notificationType.RedirectUrl = activityType.RedirectUrl;
            notificationType.AdminRedirectUrl = activityType.AdminRedirectUrl;
            return (await _context.SaveChangesAsync()) > 0;
        }
        #endregion

        public async Task<PagedResult<NotificationListDto>> GetNotifications(int appUserId, NotificationFilter filter)
        {
            // Check for permission
            if (!_currentUserService.HavePermission(Permissions.ViewNotifications))
                appUserId = _currentUserService.AppUserId;

            var notifications = _context.Notifications
                 .Include(t => t.ActivityType)
                .IgnoreQueryFilters()
                .Where(n => n.RecipientId == appUserId && n.NotificationTypeId == NotificationTypeEnum.PushNotification)
                .OrderBy(filter.SortBy)
                .AsQueryable();

            if (filter.SortBy == null || filter.SortBy.Count == 0)
                notifications = notifications.OrderByDescending(n => n.Id);

            // Check for deletedNotification
            if (!_currentUserService.HavePermission(Permissions.ViewDeletedNotifications))
                notifications = notifications.Where(p => !p.IsDeletedForRecipient);

            if (filter != null)
            {

                if (filter.ActivityTypeId.HasValue)
                    notifications = notifications.Where(p => p.ActivityTypeId == filter.ActivityTypeId);



                // Search by ticket
                if (!string.IsNullOrWhiteSpace(filter.Search))
                    notifications = notifications.Where(p =>
                               p.Title.Contains(filter.Search)
                               || p.Message.Contains(filter.Search)

                                );

                if (filter.From.HasValue)
                {
                    if (!filter.To.HasValue)
                        filter.To = _dateTime.Now;
                    notifications = notifications.Where(p => p.SentDate >= filter.From && p.SentDate <= filter.To);
                }
            }

            // AckNowlege the notificaiton
            if (appUserId == _currentUserService.AppUserId)
            {
                await notifications.Where(n => n.DeliveredDate == null).ForEachAsync(n =>
                {
                    n.DeliveredDate = _dateTime.Now;
                });

                await _context.SaveChangesAsync();
            }

            return await notifications.Select(n => new NotificationListDto
            {
                Id = n.Id,
                ActivityType = new NotificationActivityTypeDto
                {
                    Id = n.ActivityType.Id,
                    Name = n.ActivityType.Name,
                    RedirectUrl = n.ActivityType.RedirectUrl,
                    AdminRedirectUrl = n.ActivityType.AdminRedirectUrl,
                },
                ActivityTypeId = n.ActivityTypeId,
                DeliveredDate = n.DeliveredDate,
                Message = n.Message,
                ReadDate = n.ReadDate,
                RecipientId = n.RecipientId,
                SenderId = n.SenderId,
                SentDate = n.SentDate,
                SourceEntityId = n.SourceEntityId,
                Title = n.Title,
                Email = n.Email,
                IsSent = n.IsSent,
                FirstName = n.FirstName,
                LastName = n.LastName,
                PhoneNumber = n.PhoneNumber,
            }).GetPagedAsync(filter.PageNumber, filter.PageSize);


        }

        public async Task<NotificationDto> GetNotificationById(int notificationId)
        {
            var notification = await _context.Notifications
                 .Include(t => t.ActivityType)
                 .FirstOrDefaultAsync(n => n.Id == notificationId);


            // Check for permission
            if (!_currentUserService.HavePermission(Permissions.ViewNotifications))
            {
                if (notification.SenderId != _currentUserService.AppUserId && notification.RecipientId != _currentUserService.AppUserId)
                    throw new ForbiddenException();
            }

            // Check if the current user is recipient then mark the notification as read
            if (notification.RecipientId == _currentUserService.AppUserId)
            {
                notification.ReadDate = _dateTime.Now;
                await _context.SaveChangesAsync();
            }

            return new NotificationDto
            {
                Id = notification.Id,
                ActivityType = new NotificationActivityTypeDto
                {
                    Id = notification.ActivityType.Id,
                    Name = notification.ActivityType.Name,
                    RedirectUrl = notification.ActivityType.RedirectUrl,
                    AdminRedirectUrl = notification.ActivityType.AdminRedirectUrl,
                },
                ActivityTypeId = notification.ActivityTypeId,
                DeliveredDate = notification.DeliveredDate,
                Message = notification.Message,
                ReadDate = notification.ReadDate,
                RecipientId = notification.RecipientId,
                SenderId = notification.SenderId,
                SentDate = notification.SentDate,
                SourceEntityId = notification.SourceEntityId,
                Email = notification.Email,
                IsSent = notification.IsSent,
                FirstName = notification.FirstName,
                LastName = notification.LastName,
                PhoneNumber = notification.PhoneNumber,
                Title = notification.Title,
            };


        }

        public async Task<bool> MarkNotificationAsRead(int notificationId)
        {
            var notification = await _context.Notifications
                 .FirstOrDefaultAsync(n => n.Id == notificationId);


            // Check for permission
            if (notification.RecipientId != _currentUserService.AppUserId)
                if (!_currentUserService.HavePermission(Permissions.ViewNotifications))
                    throw new ForbiddenException();

            notification.ReadDate = _dateTime.Now;
            return (await _context.SaveChangesAsync()) > 0;

        }

        public async Task<bool> MarkNotificationAsAcknowledged(int notificationId)
        {
            var notification = await _context.Notifications
                 .FirstOrDefaultAsync(n => n.Id == notificationId);


            // Check for permission
            if (notification.RecipientId != _currentUserService.AppUserId)
                if (!_currentUserService.HavePermission(Permissions.ViewNotifications))
                    throw new ForbiddenException();

            notification.DeliveredDate = _dateTime.Now;
            return (await _context.SaveChangesAsync()) > 0;

        }

        public async Task<bool> DeleteNotification(int notificationId)
        {
            var notification = await _context.Notifications
                 .FirstOrDefaultAsync(n => n.Id == notificationId);

            // Check for permission
            if (notification.RecipientId != _currentUserService.AppUserId)
                if (!_currentUserService.HavePermission(Permissions.DeleteNotifications))
                    throw new ForbiddenException();

            notification.IsDeletedForRecipient = true;
            return (await _context.SaveChangesAsync()) > 0;

        }

        public async Task<bool> SendPushNotification(InsertNotificationCommand notification)
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                var notificationActivityType = await context.NotificationActivityTypes
                     .FirstOrDefaultAsync(n => n.Id == notification.ActivityTypeId);

                if (notificationActivityType == null)
                    throw new BadRequestException("Invalid activity type");


                var newNotificaiton = new Notification
                {
                    ActivityTypeId = (int)notification.ActivityTypeId,
                    Message = notification.Message,
                    RecipientId = notification.RecipientId,
                    SenderId = notification.SenderId,
                    SentDate = _dateTime.Now,
                    SourceEntityId = notification.SourceEntityId,
                    Title = notification.Title,
                    NotificationTypeId = NotificationTypeEnum.PushNotification,

                };


                context.Notifications.Add(newNotificaiton);
                await context.SaveChangesAsync();
                //_eventPublisher.Publish(new NewNotificationReceivedEvent(newNotificaiton, notificationActivityType));



                var notificationDto = new NotificationDto
                {
                    Id = newNotificaiton.Id,
                    ActivityType = new NotificationActivityTypeDto
                    {
                        Id = notificationActivityType.Id,
                        Name = notificationActivityType.Name,
                        RedirectUrl = notificationActivityType.RedirectUrl,
                        AdminRedirectUrl = notificationActivityType.AdminRedirectUrl
                    },
                    ActivityTypeId = newNotificaiton.ActivityTypeId,
                    DeliveredDate = newNotificaiton.DeliveredDate,
                    Message = newNotificaiton.Message,
                    ReadDate = newNotificaiton.ReadDate,
                    RecipientId = newNotificaiton.RecipientId,
                    SenderId = newNotificaiton.SenderId,
                    SentDate = newNotificaiton.SentDate,
                    SourceEntityId = newNotificaiton.SourceEntityId,
                    Title = newNotificaiton.Title,
                };
                await _hubNotificationHelper.SendNotificationParallel(notification.RecipientId, JsonConvert.SerializeObject(notificationDto));
            }



            return true;
        }

        public async Task<bool> Notification(SendNotificationCommand notification)
        {
            // Check if NotificationMode is available
            if (notification.EmailNotification == null && notification.PushNotification == null && notification.SMSNotification == null)
                throw new BadRequestException("Warning", "Atleast one mode of notificaiton is required");

            // Check for message validation
            if (notification.EmailNotification != null)
                if (string.IsNullOrWhiteSpace(notification.EmailNotification.Title) || string.IsNullOrWhiteSpace(notification.EmailNotification.Message))
                    throw new BadRequestException("Warning", "Both email subject and body are required");

            if (notification.SMSNotification != null)
                if (string.IsNullOrWhiteSpace(notification.SMSNotification.Message))
                    throw new BadRequestException("Warning", "Messange of sms is required");

            if (notification.PushNotification != null)
                if (string.IsNullOrWhiteSpace(notification.PushNotification.Title) || string.IsNullOrWhiteSpace(notification.PushNotification.Message))
                    throw new BadRequestException("Warning", "Both title and message of push notificaiton are required");

            if (notification.Recipients == null)
                notification.Recipients = new List<NotificationRecipient>();

            //// Check for customer Filter
            //if (notification.Customers != null)
            //{
            //    // Remove pagination
            //    notification.Customers.PageSize = null;
            //    notification.Customers.PageNumber = 1;

            //    // Get Customers
            //    var customers = await _customerService.GetCustomers(notification.Customers);

            //    if (customers != null && customers.Results != null)
            //        notification.Recipients.AddRange(customers.Results.Select(c => new NotificationRecipient
            //        {
            //            AppUserId = c.AppUserId,
            //            Email = c.Email,
            //            FirstName = c.FirstName,
            //            LastName = c.LastName,
            //            PhoneNumber = c.PhoneNumber,
            //            Username = c.IsIndividual ? $"{c.FirstName} {c.LastName}" : c.CompanyName,
            //        }));
            //}

            if (notification.Recipients.Count == 0)
                throw new BadRequestException("Warning", "Atleast one recipient is required");


            foreach (var recipient in notification.Recipients)
            {
                if (string.IsNullOrWhiteSpace(recipient.Username))
                    recipient.Username = $"{recipient.FirstName} {recipient.LastName}";

                // Send email notificaition
                if (notification.EmailNotification != null)
                {
                    // Check for email
                    if (!string.IsNullOrEmpty(recipient.Email))
                    {
                        _context.Notifications.Add(new Notification
                        {
                            ActivityTypeId = (int)NotificationActivityTypeEnum.None,
                            Email = recipient.Email,
                            FirstName = recipient.FirstName,
                            LastName = recipient.LastName,
                            Message = notification.EmailNotification.Message.ReplaceParameters(recipient),
                            PhoneNumber = recipient.PhoneNumber,
                            Title = notification.EmailNotification.Title.ReplaceParameters(recipient),
                            NotificationTypeId = NotificationTypeEnum.Email,
                            SenderId = _currentUserService.AppUserId,
                            RecipientId = recipient.AppUserId == 0 ? null : recipient.AppUserId,
                            SentDate = _dateTime.Now,

                        });
                    }

                }

                //  // Send push notificaition
                if (notification.PushNotification != null)
                {
                    // Check for email
                    if (recipient.AppUserId != null && recipient.AppUserId != 0)
                    {
                        _context.Notifications.Add(new Notification
                        {
                            ActivityTypeId = (int)NotificationActivityTypeEnum.None,
                            Email = recipient.Email,
                            FirstName = recipient.FirstName,
                            LastName = recipient.LastName,
                            PhoneNumber = recipient.PhoneNumber,
                            Message = notification.PushNotification.Message.ReplaceParameters(recipient),
                            Title = notification.PushNotification.Title.ReplaceParameters(recipient),
                            NotificationTypeId = NotificationTypeEnum.PushNotification,
                            SenderId = _currentUserService.AppUserId,
                            RecipientId = recipient.AppUserId == 0 ? null : recipient.AppUserId,
                            SentDate = _dateTime.Now,
                        });
                    }

                }


                //  // Send push notificaition
                if (notification.SMSNotification != null)
                {
                    // Check for email
                    if (!string.IsNullOrEmpty(recipient.PhoneNumber))
                    {
                        _context.Notifications.Add(new Notification
                        {
                            ActivityTypeId = (int)NotificationActivityTypeEnum.None,
                            Email = recipient.Email,
                            FirstName = recipient.FirstName,
                            LastName = recipient.LastName,
                            PhoneNumber = recipient.PhoneNumber,
                            Message = notification.SMSNotification.Message.ReplaceParameters(recipient),
                            Title = notification.SMSNotification.Title.ReplaceParameters(recipient),
                            NotificationTypeId = NotificationTypeEnum.Mobile,
                            SenderId = _currentUserService.AppUserId,
                            RecipientId = recipient.AppUserId == 0 ? null : recipient.AppUserId,
                            SentDate = _dateTime.Now,
                        });
                    }


                }
            }
            var isSucess = (await _context.SaveChangesAsync()) > 0;
            _backgroundTaskService.SendUnSentNotifications();
            return isSucess;

        }

        public async Task<List<AppUserListDto>> OnlineUsers()
        {
            var onlineUsers = _hubNotificationHelper.GetOnlineUsers();
            if (onlineUsers == null)
                onlineUsers = new List<int>();

            var appusers = _context.AppUsers
                           .Include(a => a.AppUserType)
                           .Include(a => a.AppUserRoles)
                           .AsNoTracking()
                           .Where(user => !user.IsSystemAccount && onlineUsers.Contains(user.Id));


            //appusers = from a in appusers
            //           join ou in onlineUsers
            //            on a.Id equals  ou into a_ou
            //            from aou in a_ou.DefaultIfEmpty()
            //            select a;


            return await appusers.Select(a => new AppUserListDto
            {
                AppUserType = a.AppUserType.Name,
                AppUserTypeId = a.AppUserTypeId,
                Email = a.Email,
                FirstName = a.FirstName,
                Id = a.Id,
                IsEmailVerified = a.IsEmailVerified,
                JobTitle = a.JobTitle,
                LastName = a.LastName,
                MiddleName = a.MiddleName,
                PhoneNumber = a.PhoneNumber,
                Username = a.Username,
                IsActive = a.Active,
            }).ToListAsync();
        }


        #region Notificaiton Group

        public async Task<int> InsertUpdateNotificationGroup(InsertUpdateNotificationGroup notificationGroup)
        {
            if (!_currentUserService.HavePermission(Permissions.ManageNotificationGroups))
                throw new ForbiddenException();


            if (notificationGroup.Id == 0)
            {
                var newNotificationGroup = new NotificationGroup
                {
                    Description = notificationGroup.Description,
                    Name = notificationGroup.Name,

                };
                _context.NotificationGroups.Add(newNotificationGroup);
                await _context.SaveChangesAsync();


                return newNotificationGroup.Id;
            }
            else
            {

                var existing = await _context.NotificationGroups.FindAsync(notificationGroup.Id);
                if (existing == null)
                    throw new BadRequestException("Invalid Notification Group", "Invalid notification group Id");

                existing.Description = notificationGroup.Description;
                existing.Name = notificationGroup.Name;
                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;

        }

        public async Task<NotificationGroupDto> GetNotificationGroup(int id)
        {

            var notificationGroup = await _context.NotificationGroups
                .Include(c => c.NotificationGroupSubscribedActivities)
                    .ThenInclude(a => a.NotificationActivityType)
                 .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (notificationGroup == null)
                throw new NotFoundException("NotificationGroup not found");

            if (!_currentUserService.HavePermission(Permissions.ManageNotificationGroups))
                throw new ForbiddenException();



            return new NotificationGroupDto
            {
                Description = notificationGroup.Description,
                Name = notificationGroup.Name,
                Id = notificationGroup.Id,
                SubscribedActivities = notificationGroup.NotificationGroupSubscribedActivities
                    .Select(a => new NotificationActivityTypeDto
                    {
                        Id = a.NotificationActivityType.Id,
                        Description = a.NotificationActivityType.Description,
                        DisplayName = a.NotificationActivityType.DisplayName,
                        Name = a.NotificationActivityType.Name,
                        RedirectUrl = a.NotificationActivityType.RedirectUrl,
                        GroupName = a.NotificationActivityType.GroupName,
                    }).ToList()
            };

        }

        public async Task<PagedResult<NotificationGroupDto>> GetNotificationGroups(NotificationGroupFilter filter)
        {
            if (!_currentUserService.HavePermission(Permissions.ManageNotificationGroups))
                throw new ForbiddenException();

            var notificationGroups = _context.NotificationGroups
                 .AsNoTracking()
                 .Include(c => c.NotificationGroupSubscribedActivities)
                    .ThenInclude(a => a.NotificationActivityType)
                .IgnoreDeletedNavigationProperties()
                .OrderBy(filter.SortBy)
                .AsQueryable();



            if (filter != null)
            {

                // Search by notificationGroup
                if (!string.IsNullOrWhiteSpace(filter.Search))
                    notificationGroups = notificationGroups.Where(p =>
                               p.Name.Contains(filter.Search)
                                );
            }

            return await notificationGroups.Select(notificationGroup => new NotificationGroupDto
            {
                Description = notificationGroup.Description,
                Name = notificationGroup.Name,
                Id = notificationGroup.Id,
                SubscribedActivities = notificationGroup.NotificationGroupSubscribedActivities
                    .Select(a => new NotificationActivityTypeDto
                    {
                        Id = a.NotificationActivityType.Id,
                        Description = a.NotificationActivityType.Description,
                        DisplayName = a.NotificationActivityType.DisplayName,
                        Name = a.NotificationActivityType.Name,
                        RedirectUrl = a.NotificationActivityType.RedirectUrl,
                        GroupName = a.NotificationActivityType.GroupName,
                    }).ToList()
            }).GetPagedAsync(filter.PageNumber, filter.PageSize);



        }
        public async Task<bool> DeleteNotificationGroup(int notificationGroupId)
        {

            var notificationGroup = await _context.NotificationGroups.FirstOrDefaultAsync(p => p.Id == notificationGroupId);
            if (notificationGroup == null)
                throw new BadRequestException("Invalid Notification Group");

            if (!_currentUserService.HavePermission(Permissions.ManageNotificationGroups))
                throw new ForbiddenException();



            notificationGroup.IsDeleted = true;
            return (await _context.SaveChangesAsync()) > 0;
        }


        public async Task<bool> AddNotificationGroupSubscribedActivity(InsertNotificationGroupSubscribedActivity subscribedActivity)
        {
            var notificationGroup = await _context.NotificationGroups
                .Include(n => n.NotificationGroupSubscribedActivities)
                .FirstOrDefaultAsync(p => p.Id == subscribedActivity.NotificationGroupId);

            if (notificationGroup == null)
                throw new BadRequestException("Invalid Notification Group");

            if (!_currentUserService.HavePermission(Permissions.ManageNotificationGroups))
                throw new ForbiddenException();



            foreach (var activityId in subscribedActivity.NotificationActivityTypeIds)
            {
                // Check if activity is already exist
                if (!notificationGroup.NotificationGroupSubscribedActivities.Any(n => n.NotificationActivityTypeId == activityId))
                    notificationGroup.NotificationGroupSubscribedActivities.Add(new NotificationGroupSubscribedActivity
                    {
                        NotificationActivityTypeId = activityId,
                        NotificationGroupId = subscribedActivity.NotificationGroupId
                    });
            }

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> RemoveNotificationGroupSubscribedActivity(InsertNotificationGroupSubscribedActivity subscribedActivity)
        {
            var notificationGroup = await _context.NotificationGroups
                .Include(n => n.NotificationGroupSubscribedActivities)
                .FirstOrDefaultAsync(p => p.Id == subscribedActivity.NotificationGroupId);

            if (notificationGroup == null)
                throw new BadRequestException("Invalid Notification Group");

            if (!_currentUserService.HavePermission(Permissions.ManageNotificationGroups))
                throw new ForbiddenException();



            foreach (var activityId in subscribedActivity.NotificationActivityTypeIds)
            {
                // Check if activity is already exist
                var notificaitonActivity = notificationGroup.NotificationGroupSubscribedActivities.FirstOrDefault(n => n.NotificationActivityTypeId == activityId);

                if (notificaitonActivity != null)
                    notificaitonActivity.IsDeleted = true;

            }

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<NotificationActivityTypeDto>> NotificationGroupSubscribedActivity(int notificationGroupId)
        {
            var notificationGroup = await _context.NotificationGroups
               .Include(n => n.NotificationGroupSubscribedActivities)
               .FirstOrDefaultAsync(p => p.Id == notificationGroupId);

            if (notificationGroup == null)
                throw new BadRequestException("Invalid Notification Group");

            if (!_currentUserService.HavePermission(Permissions.ManageNotificationGroups))
                throw new ForbiddenException();



            return notificationGroup.NotificationGroupSubscribedActivities.Select(s => new NotificationActivityTypeDto
            {
                Id = s.NotificationActivityType.Id,
                Description = s.NotificationActivityType.Description,
                DisplayName = s.NotificationActivityType.DisplayName,
                Name = s.NotificationActivityType.Name,
                GroupName = s.NotificationActivityType.GroupName,
                RedirectUrl = s.NotificationActivityType.RedirectUrl

            }).ToList();

        }


        public async Task<bool> AddNotificationGroupSubscriber(List<AddNotificationGroupSubscriber> subscribers)
        {
            if (subscribers == null || subscribers.Count == 0)
                throw new BadRequestException("No subscriber selected");

            var notificationGroup = await _context.NotificationGroups
                .Include(n => n.NotificationGroupSubscribers)
                .FirstOrDefaultAsync(p => p.Id == subscribers.First().NotificationGroupId);

            if (notificationGroup == null)
                throw new BadRequestException("Invalid Notification Group");

            if (!_currentUserService.HavePermission(Permissions.ManageNotificationGroups))
                throw new ForbiddenException();



            foreach (var subscriber in subscribers)
            {
                var existing = notificationGroup.NotificationGroupSubscribers.FirstOrDefault(n => n.AppUserId == subscriber.AppUserId);

                if (existing == null)
                {
                    notificationGroup.NotificationGroupSubscribers.Add(new NotificationGroupSubscriber
                    {
                        AppUserId = subscriber.AppUserId,
                        SMS = subscriber.SMS,
                        Email = subscriber.Email,
                        PushNotification = subscriber.PushNotification,

                    });
                }
                else
                {
                    existing.SMS = subscriber.SMS;
                    existing.Email = subscriber.Email;
                    existing.PushNotification = subscriber.PushNotification;
                }

            }

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> RemoveNotificationGroupSubscriber(RemoveNotificationGroupSubscriber subscriber)
        {
            var notificationGroup = await _context.NotificationGroups
                .Include(n => n.NotificationGroupSubscribers)
                .FirstOrDefaultAsync(p => p.Id == subscriber.NotificationGroupId);

            if (notificationGroup == null)
                throw new BadRequestException("Invalid Notification Group");

            if (!_currentUserService.HavePermission(Permissions.ManageNotificationGroups))
                throw new ForbiddenException();



            // Check if activity is already exist
            var notificaitonActivity = notificationGroup.NotificationGroupSubscribers.FirstOrDefault(n => n.AppUserId == subscriber.AppUserId);

            if (notificaitonActivity != null)
                notificaitonActivity.IsDeleted = true;


            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<NotificationGroupSubscriberDto>> NotificationGroupSubscribers(int notificationGroupId)
        {
            var notificationGroup = await _context.NotificationGroups
               .Include(n => n.NotificationGroupSubscribers)
                    .ThenInclude(s => s.AppUser)
               .FirstOrDefaultAsync(p => p.Id == notificationGroupId);

            if (notificationGroup == null)
                throw new BadRequestException("Invalid Notification Group");

            if (!_currentUserService.HavePermission(Permissions.ManageNotificationGroups))
                throw new ForbiddenException();



            return notificationGroup.NotificationGroupSubscribers.Select(s => new NotificationGroupSubscriberDto
            {
                AppUserId = s.AppUserId,
                Email = s.Email,
                NotificationGroupId = s.NotificationGroupId,
                PushNotification = s.PushNotification,
                SMS = s.SMS,
                FirstName = s.AppUser.FirstName,
                Username = s.AppUser.Username,
                LastName = s.AppUser.LastName,
                MiddleName = s.AppUser.MiddleName

            }).ToList();

        }

        public async Task<List<AppUserSubscribedActivityDto>> AppUserSubscribedActivities(int appuserId)
        {


            if (!_currentUserService.HavePermission(Permissions.ManageNotificationGroups))
                if (_currentUserService.AppUserId != appuserId)
                    throw new ForbiddenException();

            // Get all subscribed activity of user
            var subscribedActivities = from gs in _context.NotificationGroupSubscribers
                                    .Include(g => g.NotificationGroup)
                                       join gsa in _context.NotificationGroupSubscribedActivities
                                       on gs.NotificationGroupId equals gsa.NotificationGroupId
                                       where gs.AppUserId == appuserId
                                       select
                                       new
                                       {
                                           gs.NotificationGroup,
                                           gs.AppUserId,
                                           gs.Email,
                                           gs.SMS,
                                           gs.PushNotification,
                                           gsa.NotificationActivityType,
                                       };

            return await subscribedActivities.Select(s => new AppUserSubscribedActivityDto
            {
                ActivityTypeName = s.NotificationActivityType.DisplayName,
                Email = s.Email,
                NotificationActivityTypeId = s.NotificationActivityType.Id,
                NotificationGroupId = s.NotificationGroup.Id,
                NotificationGroupName = s.NotificationGroup.Name,
                PushNotification = s.PushNotification,
                SMS = s.SMS
            }).ToListAsync();
        }

        public async Task<bool> SendNotificationForActivity(SendNotificationForActivity notification)
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                // Get all notificaiton recipients who subscribed the activity
                var query = from gs in context.NotificationGroupSubscribers
                                    .Include(g => g.AppUser)
                                    .Include(g => g.NotificationGroup)
                            join gsa in context.NotificationGroupSubscribedActivities
                            on gs.NotificationGroupId equals gsa.NotificationGroupId
                            where gsa.NotificationActivityTypeId == (int)notification.NotificationActivityTypeId
                            select
                            new
                            {
                                gs.AppUser,
                                gs.AppUserId,
                                gs.Email,
                                gs.SMS,
                                gs.PushNotification,
                                gsa.NotificationActivityTypeId,
                                gs.AppUser.FirstName,
                                gs.AppUser.LastName,
                                gs.AppUser.PhoneNumber,
                                AppUserEmail = gs.AppUser.Email,
                            };


                var subscribers = await query
                    .Where(q => q.AppUser.AppUserTypeId == (int)AppUserTypeEnum.Internal)
                    .ToListAsync();


                var notificationActivityType = await context.NotificationActivityTypes
                 .FirstOrDefaultAsync(n => n.Id == (int)notification.NotificationActivityTypeId);

                if (notificationActivityType == null)
                    throw new BadRequestException("Invalid activity type");

                foreach (var subscriber in subscribers)
                {
                    if (subscriber.Email)
                    {
                        // Send email notificaition
                        if (notification.EmailNotification != null)
                        {
                            // Check for email
                            if (!string.IsNullOrEmpty(subscriber.AppUserEmail))
                            {
                                context.Notifications.Add(new Notification
                                {
                                    ActivityTypeId = (int)notification.NotificationActivityTypeId,
                                    Email = subscriber.AppUserEmail,
                                    FirstName = subscriber.FirstName,
                                    LastName = subscriber.LastName,
                                    Message = notification.EmailNotification.Message,
                                    PhoneNumber = subscriber.PhoneNumber,
                                    Title = notification.EmailNotification.Title,
                                    NotificationTypeId = NotificationTypeEnum.Email,
                                    SenderId = notification.SenderId,
                                    RecipientId = subscriber.AppUserId,
                                    SentDate = _dateTime.Now,
                                });
                            }

                        }

                    }



                    //  // Send push notificaition
                    if (subscriber.PushNotification)
                        if (notification.PushNotification != null)
                        {
                            context.Notifications.Add(new Notification
                            {
                                ActivityTypeId = (int)notification.NotificationActivityTypeId,
                                Email = subscriber.AppUserEmail,
                                FirstName = subscriber.FirstName,
                                LastName = subscriber.LastName,
                                PhoneNumber = subscriber.PhoneNumber,
                                Message = notification.EmailNotification.Message,
                                Title = notification.EmailNotification.Title,
                                NotificationTypeId = NotificationTypeEnum.PushNotification,
                                SenderId = notification.SenderId,
                                RecipientId = subscriber.AppUserId,
                                SentDate = _dateTime.Now,
                                SourceEntityId = notification.SourceEntityId == 0 ? null : notification.SourceEntityId,

                            });
                        }




                    //  // Send push notificaition
                    if (subscriber.SMS)
                        if (notification.SMSNotification != null)
                        {
                            // Check for email
                            if (!string.IsNullOrEmpty(subscriber.PhoneNumber))
                            {
                                context.Notifications.Add(new Notification
                                {
                                    ActivityTypeId = (int)NotificationActivityTypeEnum.None,
                                    Email = subscriber.AppUserEmail,
                                    FirstName = subscriber.FirstName,
                                    LastName = subscriber.LastName,
                                    PhoneNumber = subscriber.PhoneNumber,
                                    Message = notification.SMSNotification.Message,
                                    Title = notification.SMSNotification.Title,
                                    NotificationTypeId = NotificationTypeEnum.Mobile,
                                    SenderId = notification.SenderId,
                                    RecipientId = subscriber.AppUserId,
                                    SentDate = _dateTime.Now,
                                    SourceEntityId = notification.SourceEntityId
                                });
                            }

                        }
                }

                await context.SaveChangesAsync();

                var backgroundService = scope.ServiceProvider.GetRequiredService<IBackgroundTaskService>();
                backgroundService.SendUnSentNotifications();
            }
            return true;

        }
        #endregion




    }
}
