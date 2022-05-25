using PreSchool.Application;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.Addresses;
using PreSchool.Application.Services.Addresses.Models.Commands;
using PreSchool.Application.Services.Addresses.Models.Dtos;
using PreSchool.Application.Services.Addresses.Models.Filters;
using PreSchool.Application.Services.Logs;
using PreSchool.Application.Services.Logs.Models;
using PreSchool.Application.Services.Settings;
using PreSchool.Application.Services.Settings.Models;
using PreSchool.Application.Services.Settings.Models.Command;
using PreSchool.Application.Services.Settings.Models.Dtos;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logsService;

        public LogsController(ILogService logsService)
        {
            _logsService = logsService;
        }

    
        [HttpGet]
        public async Task<PagedResult<LogDto>> Logs([FromQuery] LogFilter filter)
        {
            return await _logsService.GetAllLogs(filter);
        }

       


    }
}