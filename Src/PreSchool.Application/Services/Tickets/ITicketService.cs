using PreSchool.Application.Models;
using PreSchool.Application.Services.Tickets.Models.Commands;
using PreSchool.Application.Services.Tickets.Models.Dtos;
using PreSchool.Application.Services.Tickets.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Tickets
{
    public interface ITicketService
    {
        Task<bool> ChangeTicketStatus(ChangeTicketStatus status);
        Task<bool> DeleteTicket(int ticketId);
        Task<bool> DeleteTicketAttachment(int ticketId, int id);
        Task<bool> DeleteTicketReply(int ticketId, int id);
        Task<bool> DeleteTicketReplyAttachment(int ticketId, int replyId, int id);
        Task<TicketDto> GetTicket(int id);
        Task<FileDetail> GetTicketAttachment(int ticketId, int id);
        Task<List<TicketReplyDto>> GetTicketReply(int ticketId);
        Task<FileDetail> GetTicketReplyAttachment(int ticketId, int replyId, int id);
        Task<PagedResult<TicketListDto>> GetTickets(TicketFilter filter);
        Task<List<TicketUserDto>> GetTicketUsers(int ticketId);
        Task<List<TicketUserDto>> GetAppUsersAssignedTicket(int userId);
        Task<bool> InsertTicketAttachment(InsertTicketAttachment attachment);
        Task<bool> InsertTicketReplyAttachment(InsertTicketReplyAttachment attachment);
        Task<bool> InsertTicketUser(AddRemoveTicketUser newTicketUser);
        Task<int> InsertUpdateTicket(InsertUpdateTicket ticket);
        Task<int> InsertUpdateTicketReply(InsertUpdateTicketReply ticketReply);
        NewTicketViewModel NewTicketViewModel();
        Task<bool> RemoveTicketUser(int ticketId, int appuserId);
    }
}