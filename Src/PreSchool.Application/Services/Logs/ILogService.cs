using PreSchool.Application.Services.Logs.Models;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Logs
{
    public interface ILogService
    {
        Task<PagedResult<LogDto>> GetAllLogs(LogFilter filter);
    }
}