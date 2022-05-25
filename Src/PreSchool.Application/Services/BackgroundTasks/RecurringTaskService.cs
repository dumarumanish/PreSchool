using PreSchool.Application.Events;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.Tickets;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.BackgroundTasks
{
    public class RecurringTaskService : IRecurringTaskService
    {

        private readonly IApplicationDbContext _context;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IDateTime _dateTime;
        private readonly AppSettings _appSettings;
        private readonly IEventPublisher _eventPublisher;



        public RecurringTaskService(
           IApplicationDbContext context,
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager,
             IDateTime dateTime,
            IOptions<AppSettings> appSettings,
            IEventPublisher eventPublisher


            )
        {
            _context = context;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
            _dateTime = dateTime;
            _appSettings = appSettings.Value;
            _eventPublisher = eventPublisher;

        }
        public void InitializeRecurringJob()
        {
           

            //_recurringJobManager.AddOrUpdate(
            //   "SyncCustomers",
            //    () => SyncCustomers(),
            //     "0 4 * * *",
            //     TimeZoneInfo.Local
            //   );

        }

        //public async Task SyncCustomers()
        //{
        //    var isps = await _context.ISPs
        //       .AsNoTracking()
        //       .ToListAsync();
        //    if (isps == null || isps.Count == 0)
        //        return;

        //    foreach (var isp in isps)
        //    {
        //        try
        //        {
        //            await _customerService.SyncCustomer(isp, new Customers.Models.Filters.SyncCustomerFilter
        //            {
        //                SyncAll = true
        //            });
        //        }
        //        catch
        //        {
        //            // Skip to next isp
        //        }
        //    }
        //}

      

    }
}
