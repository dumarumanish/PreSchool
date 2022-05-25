using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Services.Logs.Models;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Enumerations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Logs
{
    public class LogService : ILogService
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public LogService(
            IApplicationDbContext context,
            ICurrentUserService currentUserService,
            IDateTime dateTime

            )
        {
            _context = context;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public async Task<PagedResult<LogDto>> GetAllLogs(LogFilter filter)
        {

           

            if (!_currentUserService.HavePermission(Permissions.ViewOthersActivityLogs))
                filter.AppUserId = _currentUserService.AppUserId;

            var logs = _context.Logs
                          .Include(l => l.ActivityType)
                          .Include(l => l.AppUser)
                          .IgnoreQueryFilters()
                          .AsNoTracking();
            if (filter != null)
            {

                if (filter.AppUserId.HasValue)
                    logs = logs.Where(p => p.AppUserId == filter.AppUserId);

                if (filter.ActivityTypeId.HasValue)
                    logs = logs.Where(p => p.ActivityTypeId == filter.ActivityTypeId);

                if (filter.From.HasValue)
                {
                    if (!filter.To.HasValue)
                        filter.To = _dateTime.Now;
                    logs = logs.Where(p => p.TimeStamp >= filter.From && p.TimeStamp <= filter.To);
                }

                if (!_currentUserService.HavePermission(Permissions.ViewErrorLogs))
                    logs = logs.Where(p => p.Level == "Information");

            }

            return await logs.Select(c => new LogDto
            {
                Id = c.Id,
                AppUser = c.AppUserId == null ? null : c.AppUser.Username,
                ActivityType = c.ActivityType.DisplayName,
                ActivityTypeId = c.ActivityTypeId,
                AppUserId = c.AppUserId,
                Exception = c.Exception,
                Level = c.Level,
                Message = c.Message,
                TimeStamp = c.TimeStamp,
                Parameters = c.Parameters,
            }).OrderBy(filter.SortBy).GetPagedAsync(filter.PageNumber, filter.PageSize);
        }



    }
}
