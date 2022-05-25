using Microsoft.AspNetCore.Mvc;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.Tickets;
using PreSchool.Application.Services.Tickets.Models.Commands;
using PreSchool.Application.Services.Tickets.Models.Dtos;
using PreSchool.Data.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreSchool.Application.Services.Tickets.Models.Filters;
using PreSchool.Application;
using PreSchool.Application.Models;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        #region Ticket

        [HttpPost]
        // [AuthorizeUser(Permissions.AddTickets)]
        //[VerifyReCaptcha]
        public async Task<int> InsertTicket(InsertUpdateTicket Ticket)
        {
            if (Ticket == null)
                throw new BadRequestException("General ticket is required.");
            Ticket.Id = 0;
            return await _ticketService.InsertUpdateTicket(Ticket);
        }

        [HttpPut("{id}")]
         // [AuthorizeUser(Permissions.UpdateTickets)]
        public async Task<int> UpdateTicket(int id, InsertUpdateTicket Ticket)
        {
            if (Ticket == null)
                throw new BadRequestException("Ticket is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id");

            if (id != Ticket.Id)
                throw new BadRequestException("Id doesnot match");
            return await _ticketService.InsertUpdateTicket(Ticket);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Sort Example : api/Tickets?SortBy[0].Name=columnName1&amp;SortBy[0].Desc=true&amp;SortBy[1].Name=columnName2
        /// </remarks>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
         // [AuthorizeUser(Permissions.ViewTickets)]
        public async Task<PagedResult<TicketListDto>> GetTicketList([FromQuery] TicketFilter filter)
        {
            return await _ticketService.GetTickets(filter);

        }

        [HttpGet("{id}")]
       // [AuthorizeUser(Permissions.ViewTickets)]
        public async Task<TicketDto> GetTicket(int id)
        {
            return await _ticketService.GetTicket(id);

        }

        [HttpDelete("{id}")]
       // [AuthorizeUser(Permissions.DeleteTickets)]
        public async Task<bool> DeleteTicket(int id)
        {
            return await _ticketService.DeleteTicket(id);

        }

        [HttpPost("{ticketId}/ChangeStatus")]
        public async Task<bool> ChangeTicketStatus(int ticketId, ChangeTicketStatus status)
        {
            if (ticketId != status.TicketId)
                throw new BadRequestException("Invalid Id, Id doesnot match");
            return await _ticketService.ChangeTicketStatus(status);

        }

        [HttpGet("ViewModel")]
        public NewTicketViewModel NewTicketViewModel()
        {
            return _ticketService.NewTicketViewModel();
        }
        #endregion

        #region Ticket Attachment


        [HttpGet("{ticketId}/Attachments/{id}")]
       // [AuthorizeUser(Permissions.ViewTickets)]
        public async Task<IActionResult> DownloadAttachment(int ticketId, int id)
        {
            var file = await _ticketService.GetTicketAttachment(ticketId, id);
            return File(file.FileContents, file.ContentType, file.FileName);

        }

        [HttpPost("{ticketId}/Attachments")]
       // [AuthorizeUser(Permissions.AddTickets)]
        public async Task<bool> InsertAttachment(int ticketId, [FromForm] InsertTicketAttachment insertAttachment)
        {
            if (insertAttachment == null)
                throw new BadRequestException("Attachment is required");

            if (ticketId != insertAttachment.ticketId)
                throw new BadRequestException("Invalid Id, Id doesnot match");

            return await _ticketService.InsertTicketAttachment(insertAttachment);
        }

        [HttpDelete("{ticketId}/Attachments/{id}")]
       // [AuthorizeUser(Permissions.UpdateTickets)]
        public async Task<bool> DeleteTicketAttachment(int ticketId, int id)
        {
            return await _ticketService.DeleteTicketAttachment(ticketId, id);
        }

        #endregion

        #region  Ticket


        [HttpGet("{ticketId}/Replies")]
        public async Task<List<TicketReplyDto>> GetTicketReplies(int ticketId)
        {
            return await _ticketService.GetTicketReply(ticketId);

        }

        [HttpPost("{ticketId}/Replies")]
        // [AuthorizeUser(Permissions.CommentOnAssignedTickets, Permissions.CommentOnAnyTickets)]
        public async Task<int> InsertTicketReply(int ticketId, InsertUpdateTicketReply insertTicket)
        {
            if (insertTicket == null)
                throw new BadRequestException("Ticket is required");

            if (ticketId != insertTicket.TicketId)
                throw new BadRequestException("Invalid Id, Id doesnot match");

            insertTicket.Id = 0;

            return await _ticketService.InsertUpdateTicketReply(insertTicket);
        }


        [HttpPut("{ticketId}/Replies/{id}")]
        // [AuthorizeUser(Permissions.CommentOnAssignedTickets, Permissions.CommentOnAnyTickets)]
        public async Task<int> UpdateTicketReply(int ticketId, int id, InsertUpdateTicketReply insertTicket)
        {
            if (insertTicket == null)
                throw new BadRequestException("Ticket is required");

            if (ticketId != insertTicket.TicketId)
                throw new BadRequestException("Invalid Id, Ticket Id doesnot match");

            if (id != insertTicket.Id)
                throw new BadRequestException("Invalid Id, Id doesnot match");

            return await _ticketService.InsertUpdateTicketReply(insertTicket);
        }

        [HttpDelete("{ticketId}/Replies/{id}")]
        // [AuthorizeUser(Permissions.DeleteAnyCommentOnTickets, Permissions.DeleteMyCommentOnTickets)]
        public async Task<bool> DeleteTicketReply(int ticketId, int id)
        {
            return await _ticketService.DeleteTicketReply(ticketId, id);
        }


        #region Ticket Message Attachment


        [HttpGet("{ticketId}/Replies/{replyId}/Attachments/{id}")]
        // [AuthorizeUser(Permissions.CommentOnAssignedTickets, Permissions.CommentOnAnyTickets)]
        public async Task<IActionResult> DownloadTicketReplyAttachment(int ticketId, int replyId, int id)
        {
            var file = await _ticketService.GetTicketReplyAttachment(ticketId, replyId, id);
            return File(file.FileContents, file.ContentType, file.FileName);

        }

        [HttpPost("{ticketId}/Replies/{replyId}/Attachments")]
        // [AuthorizeUser(Permissions.CommentOnAssignedTickets, Permissions.CommentOnAnyTickets)]
        public async Task<bool> InsertTicketRepliesAttachment(int ticketId, int replyId, [FromForm] InsertTicketReplyAttachment insertAttachment)
        {
            if (insertAttachment == null)
                throw new BadRequestException("Attachment is required");

            if (ticketId != insertAttachment.ticketId)
                throw new BadRequestException("Invalid Id, Ticket Id doesnot match");

            if (replyId != insertAttachment.ticketReplyId)
                throw new BadRequestException("Invalid Id, Message Id doesnot match");

            return await _ticketService.InsertTicketReplyAttachment(insertAttachment);
        }

        [HttpDelete("{ticketId}/Replies/{replyId}/Attachments/{id}")]
        // [AuthorizeUser(Permissions.CommentOnAssignedTickets, Permissions.CommentOnAnyTickets)]
        public async Task<bool> DeleteTicketRepliesAttachment(int ticketId, int replyId, int id)
        {
            return await _ticketService.DeleteTicketReplyAttachment(ticketId, replyId, id);
        }

        #endregion
        #endregion

        #region Ticket User


        [HttpGet("{ticketId}/Users")]
        public async Task<List<TicketUserDto>> GetTicketUsers(int ticketId)
        {
            return await _ticketService.GetTicketUsers(ticketId);

        }

        [HttpPost("{ticketId}/Users")]
        public async Task<bool> InsertTicketUser(int ticketId, AddRemoveTicketUser insertUser)
        {
            if (insertUser == null)
                throw new BadRequestException("User is required");

            if (ticketId != insertUser.TicketId)
                throw new BadRequestException("Invalid Id, Id doesnot match");

            return await _ticketService.InsertTicketUser(insertUser);
        }

        [HttpDelete("{ticketId}/Users/{id}")]
        public async Task<bool> DeleteTicketUser(int ticketId, int id)
        {
            return await _ticketService.RemoveTicketUser(ticketId, id);
        }

        /// <summary>
        /// View list of tickets that are assigned to appUser
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        [HttpGet("AppUsers/{appUserId}")]
        [AuthorizeUser]
        public async Task<List<TicketUserDto>> GetAppUsersAssignedTicket(int appUserId)
        {
            return await _ticketService.GetAppUsersAssignedTicket(appUserId);

        }

        #endregion
    }
}