using PreSchool.Application.Events;
using PreSchool.Application.Exceptions;
using PreSchool.Application.HelperClasses;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Application.Services.Emails;
using PreSchool.Application.Services.Files;
using PreSchool.Application.Services.Tickets.Events;
using PreSchool.Application.Services.Tickets.Models.Commands;
using PreSchool.Application.Services.Tickets.Models.Dtos;
using PreSchool.Application.Services.Tickets.Models.Filters;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.Tickets;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Tickets
{
    public class TicketService : ITicketService
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileService _fileService;
        private readonly IDateTime _dateTime;
        private readonly IEventPublisher _eventPublisher;
        private readonly IEmailService _emailService;
        public TicketService(
             IApplicationDbContext context,
            ICurrentUserService currentUserService,
            IFileService fileService,
            IDateTime dateTime,
            IEventPublisher eventPublisher,
            IEmailService emailService)
        {
            _context = context;
            _currentUserService = currentUserService;
            _fileService = fileService;
            _dateTime = dateTime;
            _eventPublisher = eventPublisher;
            _emailService = emailService;
        }

        #region Ticket

        public async Task<int> InsertUpdateTicket(InsertUpdateTicket ticket)
        {

            if (ticket.Id == 0)
            {
                if (_currentUserService.IsAuthenticated)
                    if (!_currentUserService.HavePermission(Permissions.AddTickets))
                        throw new ForbiddenException();

                var newTicket = new Ticket
                {
                    AppUserId = _currentUserService.IsAuthenticated ? _currentUserService.AppUserId : (int?)null,
                    Subject = ticket.Subject,
                    DepartmentId = ticket.DepartmentId,
                    DepartmentServiceId = ticket.DepartmentServiceId > 0 ? ticket.DepartmentServiceId : null,
                    Email = ticket.Email,
                    FullName = ticket.FullName,
                    PriorityId = ticket.PriorityId,
                    StatusId = TicketStatusEnum.Open,
                    Message = ticket.Message,

                };
                _context.Tickets.Add(newTicket);
                await _context.SaveChangesAsync();

                // Generate ticket code
                var ticketCount = await _context.Tickets
                    .IgnoreQueryFilters()
                    .CountAsync(t => t.Id <= newTicket.Id
                    );

                newTicket.TicketNumber = ticketCount.ToString();
                await _context.SaveChangesAsync();

                //_eventPublisher.Publish(new TicketRegisteredEvent(newTicket));
                //_eventPublisher.Publish(new TicketPostEvent(newTicket));

                _eventPublisher.Publish(new TicketOpenEvent(newTicket));

                return newTicket.Id;
            }
            else
            {
                if (!_currentUserService.HavePermission(Permissions.UpdateTickets))
                    throw new ForbiddenException();

                var existing = await _context.Tickets.FindAsync(ticket.Id);
                if (existing == null)
                    throw new BadRequestException("Invalid Ticket", "Invalid ticket Id");
                existing.Subject = ticket.Subject;
                existing.Message = ticket.Message;
                existing.DepartmentId = ticket.DepartmentId;
                existing.PriorityId = ticket.PriorityId;
                existing.DepartmentServiceId = ticket.DepartmentServiceId > 0 ? ticket.DepartmentServiceId : null;
                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;

        }

        public async Task<bool> ChangeTicketStatus(ChangeTicketStatus status)
        {
            var ticket = await _context.Tickets
                .Include(a=>a.AppUser)
                .FirstOrDefaultAsync(p => p.Id == status.TicketId);
            if (ticket == null)
                throw new BadRequestException("Invalid Ticket");

            if (ticket.AppUserId != _currentUserService.AppUserId)
                if (!_currentUserService.HavePermission(Permissions.ChangeTicketsStatus))
                    throw new ForbiddenException();

            ticket.StatusId = status.TicketStatusId;

            //send email and notification
            _eventPublisher.Publish(new TicketPostEvent(ticket));

            return (await _context.SaveChangesAsync()) > 0;
        }




        public async Task<TicketDto> GetTicket(int id)
        {

            var ticket = await _context.Tickets
                 .AsNoTracking()
                 .Include(t => t.Department)
                 .Include(t => t.DepartmentService)
                .Include(t => t.TicketAttachments)
                    .ThenInclude(a => a.File)
                .Include(t => t.TicketReplies)
                    .ThenInclude(t => t.TicketReplyAttachments)
                        .ThenInclude(a => a.File)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (ticket == null)
                throw new NotFoundException("Ticket not found");

            if (ticket.AppUserId != _currentUserService.AppUserId)
                if (!_currentUserService.HavePermission(Permissions.ViewTickets))
                    throw new ForbiddenException();
            return new TicketDto
            {
                Id = ticket.Id,
                AppUserId = ticket.AppUserId,
                Subject = ticket.Subject,
                DepartmentId = ticket.DepartmentId,
                Department = ticket.Department.Name,
                DepartmentServiceId = ticket.DepartmentServiceId,
                DepartmentService = ticket.DepartmentService?.Name,
                Email = ticket.Email,
                FullName = ticket.FullName,
                PriorityId = ticket.PriorityId,
                Priority = ticket.PriorityId.ToNameString(),
                StatusId = ticket.StatusId,
                Status = ticket.StatusId.ToNameString(),
                Message = ticket.Message,
                CreatedOn = ticket.CreatedOn,
                LastRepliedOn = ticket.LastRepliedOn,
                LastReplier = ticket.LastReplier,
                TicketNumber = ticket.TicketNumber,
                Attachments = ticket.TicketAttachments.Select(a => new TicketAttachmentDto
                {
                    AttachmentLocation = a.File.Path,
                    AttachmentName = a.AttachmentName,
                    Id = a.Id,
                    TicketId = a.TicketId,

                }).ToList(),
                Replies = ticket.TicketReplies.Select(r => new TicketReplyDto
                {
                    AppUserId = r.AppUserId,
                    CreatedOn = r.CreatedOn,
                    Email = r.Email,
                    FullName = r.FullName,
                    Id = r.Id,
                    Message = r.Message,
                    TicketId = r.TicketId,
                    ReplyAttachments = r.TicketReplyAttachments.Select(a => new TicketReplyAttachmentDto
                    {
                        AttachmentLocation = a.File.Path,
                        AttachmentName = a.AttachmentName,
                        Id = a.Id,
                        TicketId = a.TicketReply.TicketId,
                        TicketReplyId = a.TicketReplyId,
                    }).ToList()
                }).ToList()
            };

        }



        public async Task<PagedResult<TicketListDto>> GetTickets(TicketFilter filter)
        {
            var tickets = _context.Tickets
                 .AsNoTracking()
                 .Include(t => t.Department)
                 .Include(t => t.DepartmentService)
                .OrderBy(filter.SortBy)
                .AsQueryable();

            if (filter != null)
            {
                if (filter.DepartmentId.HasValue)
                    tickets = tickets.Where(p => p.DepartmentId == filter.DepartmentId);

                if (filter.DepartmentServiceId.HasValue)
                    tickets = tickets.Where(p => p.DepartmentServiceId == filter.DepartmentServiceId);

                if (filter.PriorityId.HasValue)
                    tickets = tickets.Where(p => p.PriorityId == filter.PriorityId);

                if (filter.StatusId.HasValue)
                    tickets = tickets.Where(p => p.StatusId == filter.StatusId);

                // Search by ticket
                if (!string.IsNullOrWhiteSpace(filter.Search))
                    tickets = tickets.Where(p =>
                               p.TicketNumber.Contains(filter.Search)
                               || p.FullName.Contains(filter.Search)
                               || p.Email.Contains(filter.Search)
                                );

                if (filter.From.HasValue)
                {
                    if (!filter.To.HasValue)
                        filter.To = _dateTime.Now;
                    tickets = tickets.Where(p => p.CreatedOn >= filter.From && p.CreatedOn <= filter.To);
                }
            }



            if (!_currentUserService.HavePermission(Permissions.ViewTickets))
                tickets = tickets.Where(p => p.AppUserId == _currentUserService.AppUserId);


            return await tickets.Select(ticket => new TicketListDto
            {
                Id = ticket.Id,
                AppUserId = ticket.AppUserId,
                Subject = ticket.Subject,
                DepartmentId = ticket.DepartmentId,
                Department = ticket.Department.Name,
                DepartmentServiceId = ticket.DepartmentServiceId,
                DepartmentService = ticket.DepartmentService == null ? null : ticket.DepartmentService.Name,
                Email = ticket.Email,
                FullName = ticket.FullName,
                PriorityId = ticket.PriorityId,
                Priority = ticket.PriorityId.ToNameString(),
                StatusId = ticket.StatusId,
                Status = ticket.StatusId.ToNameString(),
                CreatedOn = ticket.CreatedOn,
                TicketNumber = ticket.TicketNumber,
                LastRepliedOn = ticket.LastRepliedOn,
                LastReplier = ticket.LastReplier,
            }).GetPagedAsync(filter.PageNumber, filter.PageSize);



        }
        public async Task<bool> DeleteTicket(int ticketId)
        {

            var ticket = await _context.Tickets.FirstOrDefaultAsync(p => p.Id == ticketId);
            if (ticket == null)
                throw new BadRequestException("Invalid Ticket");

            if (!_currentUserService.HavePermission(Permissions.DeleteTickets))
                throw new ForbiddenException();


            ticket.IsDeleted = true;
            return (await _context.SaveChangesAsync()) > 0;
        }

        public NewTicketViewModel NewTicketViewModel()
        {
            var vm = new NewTicketViewModel();

            vm.TicketPriorities = EnumHelper.GetEnumValues<TicketPriorityEnum>();
            vm.TicketStatuses = EnumHelper.GetEnumValues<TicketStatusEnum>();

            return vm;
        }

        #endregion


        #region Ticket Attachment

        public async Task<bool> InsertTicketAttachment(InsertTicketAttachment attachment)
        {

            if (attachment == null || attachment.attachment == null)
                throw new BadRequestException("No attachment");


            var ticket = await _context.Tickets
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == attachment.ticketId);

            if (ticket == null)
                throw new BadRequestException("Invalid Ticket");


            var newAttachment = new TicketAttachment
            {
                TicketId = ticket.Id,
                AttachmentName = attachment.attachmentName,
                FileId = await _fileService.InsertFile(new Files.Models.InsertFileCommand { EntityName = nameof(TicketAttachment), File = attachment.attachment })

            };

            _context.TicketAttachments.Add(newAttachment);

            return (await _context.SaveChangesAsync()) > 0;

        }

        public async Task<bool> DeleteTicketAttachment(int ticketId, int id)
        {
            var ticket = await _context.Tickets
                .Include(s => s.TicketAttachments).
                FirstOrDefaultAsync(s => s.Id == ticketId);

            if (ticket == null)
                throw new NotFoundException("Ticket not found");

            if (!_currentUserService.HavePermission(Permissions.DeleteTickets))
                throw new ForbiddenException();


            var ticketAttachment = ticket.TicketAttachments.FirstOrDefault(d => d.Id == id);
            if (ticketAttachment == null)
                throw new BadRequestException("Invalid ticket");

            ticketAttachment.IsDeleted = true;
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<FileDetail> GetTicketAttachment(int ticketId, int id)
        {
            var attachment = await _context.TicketAttachments
                .OrderByDescending(d => d.CreatedOn)
                .FirstOrDefaultAsync(d => d.TicketId == ticketId && d.Id == id);

            if (attachment == null)
                throw new NotFoundException("No attachment found");

            return await _fileService.GetBinaryFile(attachment.FileId);
        }
        #endregion

        #region TicketReply

        public async Task<int> InsertUpdateTicketReply(InsertUpdateTicketReply ticketReply)
        {

            var ticket = await _context.Tickets
                .Include(t => t.TicketReplies)
                .FirstOrDefaultAsync(t => t.Id == ticketReply.TicketId);

            if (ticket == null)
                throw new NotFoundException("Ticket not found");

            // TODO : Check permisison

            if (ticket.AppUserId != _currentUserService.AppUserId)
                if (!_currentUserService.HavePermission(Permissions.ReplyTickets))
                    throw new ForbiddenException();


            if (ticketReply.Id == 0)
            {

                var reply = new TicketReply
                {
                    Message = ticketReply.Message,
                    TicketId = ticketReply.TicketId,
                    Email = ticketReply.Email,
                    FullName = ticketReply.FullName,
                    AppUserId = _currentUserService.IsAuthenticated ? _currentUserService.AppUserId : (int?)null,
                };

                _context.TicketReplies.Add(reply);

                // Change the ticket status to open if closed
                if (ticket.StatusId == TicketStatusEnum.Closed)
                    ticket.StatusId = TicketStatusEnum.Open;

                // Change last replier
                ticket.LastReplier = ticketReply.FullName;
                ticket.LastRepliedOn = _dateTime.Now;

                await _context.SaveChangesAsync();

                _eventPublisher.Publish(new TicketReplyEvent(ticket, ticketReply.Message));

                return ticket.Id;
            }
            else
            {
                var existingType = ticket.TicketReplies.FirstOrDefault(r => r.Id == ticketReply.Id);
                if (existingType == null)
                    throw new BadRequestException("Invalid TicketReply", "Invalid ticketReply Id");
                existingType.Message = ticketReply.Message;
                if (await _context.SaveChangesAsync() > 0)
                    return existingType.Id;
            }
            return 0;
        }


        public async Task<List<TicketReplyDto>> GetTicketReply(int ticketId)
        {

            var ticket = await _context.Tickets
                 .AsNoTracking()
                .Include(t => t.TicketReplies)
                    .ThenInclude(t => t.TicketReplyAttachments)
                        .ThenInclude(a => a.File)
                .FirstOrDefaultAsync(p => p.Id == ticketId);

            if (ticket == null)
                throw new NotFoundException("Ticket not found");

            return ticket.TicketReplies.Select(r => new TicketReplyDto
            {
                AppUserId = r.AppUserId,
                CreatedOn = r.CreatedOn,
                Email = r.Email,
                FullName = r.FullName,
                Id = r.Id,
                Message = r.Message,
                TicketId = r.TicketId,
                ReplyAttachments = r.TicketReplyAttachments.Select(a => new TicketReplyAttachmentDto
                {
                    AttachmentLocation = a.File.Path,
                    AttachmentName = a.AttachmentName,
                    Id = a.Id,
                    TicketId = a.TicketReply.TicketId,
                    TicketReplyId = a.TicketReplyId,
                }).ToList()
            }).ToList();

        }


        public async Task<bool> DeleteTicketReply(int ticketId, int id)
        {

            var ticketReply = await _context.TicketReplies
                .Include(t => t.Ticket)
                .FirstOrDefaultAsync(p => p.Id == id && p.TicketId == ticketId);
            if (ticketReply == null)
                throw new BadRequestException("Invalid Ticket Reply");

            if (!_currentUserService.HavePermission(Permissions.DeleteTicketsReply))
                throw new ForbiddenException();


            ticketReply.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }


        #region Ticket Message Attachment

        public async Task<bool> InsertTicketReplyAttachment(InsertTicketReplyAttachment attachment)
        {

            if (attachment == null || attachment.attachment == null)
                throw new BadRequestException("No attachment");

            var ticketReply = await _context.TicketReplies
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == attachment.ticketReplyId && s.TicketId == attachment.ticketId);

            if (ticketReply == null)
                throw new BadRequestException("Invalid Ticket Message");

            var newAttachemnt =
                           new TicketReplyAttachment
                           {
                               TicketReplyId = ticketReply.Id,
                               AttachmentName = attachment.attachmentName,
                               FileId = await _fileService.InsertFile(new Files.Models.InsertFileCommand { EntityName = nameof(TicketReplyAttachment), File = attachment.attachment })

                           };
            _context.TicketReplyAttachments.Add(newAttachemnt);

            return (await _context.SaveChangesAsync()) > 0;

        }

        public async Task<bool> DeleteTicketReplyAttachment(int ticketId, int replyId, int id)
        {
            var ticket = await _context.TicketReplies
                .Include(s => s.TicketReplyAttachments)
                .FirstOrDefaultAsync(s => s.Id == replyId && s.TicketId == ticketId);

            if (ticket == null)
                throw new NotFoundException("Ticket message not found");

            var ticketAttachment = ticket.TicketReplyAttachments.FirstOrDefault(d => d.Id == id);
            if (ticketAttachment == null)
                throw new BadRequestException("Invalid Attachment");

            ticketAttachment.IsDeleted = true;
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<FileDetail> GetTicketReplyAttachment(int ticketId, int replyId, int id)
        {
            var attachment = await _context.TicketReplyAttachments
                .Include(t => t.TicketReply)
                .FirstOrDefaultAsync(d => d.Id == id && d.TicketReplyId == replyId && d.TicketReply.TicketId == ticketId);

            if (attachment == null)
                throw new NotFoundException("No attachment found");
            return await _fileService.GetBinaryFile(attachment.FileId);
        }
        #endregion
        #endregion


        #region Ticket Users

        public async Task<bool> InsertTicketUser(AddRemoveTicketUser newTicketUser)
        {


            // Check if ticket is 
            var ticket = await _context.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == newTicketUser.TicketId);

            // Check if user is 
            var user = await _context.AppUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == newTicketUser.AppUserId);

            // Check if user is already assigned to the ticket
            var existing = await _context.TicketUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.AppUserId == newTicketUser.AppUserId && p.TicketId == newTicketUser.TicketId);

            if ((ticket) == null)
                throw new BadRequestException("Invalid Ticket");

            if ((user) == null)
                throw new BadRequestException("Invalid User");

            if (existing != null)
                throw new BadRequestException("User is already assigned to this ticket");

            if (!_currentUserService.HavePermission(Permissions.ManageAssigneeForTickets))
                throw new ForbiddenException();

          

            _context.TicketUsers.Add(new TicketUser
            {
                AppUserId = newTicketUser.AppUserId,
                TicketId = newTicketUser.TicketId,

            });
            //_emailService.AddedToTicket(new EmailTemplates.ViewModels.Tickets.AddedToTicketEmailViewModel
            //    (
            //   (user)?.Username,
            //    (ticket)?.Subject,
            //    (user)?.Email

            //));

            return (await _context.SaveChangesAsync()) > 0;

        }

        public async Task<List<TicketUserDto>> GetTicketUsers(int ticketId)
        {
            return await _context.TicketUsers
               .Include(t => t.AppUser)
               .Include(t => t.Ticket)
                   .ThenInclude(t => t.Department)
               .Include(t => t.Ticket)
                   .ThenInclude(t => t.DepartmentService)
                .AsNoTracking()
                .Select(user => new TicketUserDto
                {
                    Id = user.Id,
                    AppUserId = user.AppUserId,
                    AppUsername = user.AppUser.Username,
                    TicketId = user.TicketId,
                    AssignedOn = user.CreatedOn,
                    Ticket = new TicketListDto
                    {
                        Id = user.Ticket.Id,
                        AppUserId = user.Ticket.AppUserId,
                        Subject = user.Ticket.Subject,
                        DepartmentId = user.Ticket.DepartmentId,
                        Department = user.Ticket.Department.Name,
                        DepartmentServiceId = user.Ticket.DepartmentServiceId,
                        DepartmentService = user.Ticket.DepartmentService == null ? null : user.Ticket.DepartmentService.Name,
                        Email = user.Ticket.Email,
                        FullName = user.Ticket.FullName,
                        PriorityId = user.Ticket.PriorityId,
                        Priority = user.Ticket.PriorityId.ToNameString(),
                        StatusId = user.Ticket.StatusId,
                        Status = user.Ticket.StatusId.ToNameString(),
                        CreatedOn = user.Ticket.CreatedOn,
                        TicketNumber = user.Ticket.TicketNumber,
                        LastRepliedOn = user.Ticket.LastRepliedOn,
                        LastReplier = user.Ticket.LastReplier,
                    }
                }).Where(u => u.TicketId == ticketId)
                .ToListAsync();

        }

        public async Task<List<TicketUserDto>> GetAppUsersAssignedTicket(int userId)
        {
            if (!_currentUserService.HavePermission(Permissions.ViewAssignedUsersTickets))
                if (_currentUserService.AppUserId != userId)
                    throw new ForbiddenException();

            return await _context.TicketUsers
                        .Include(p => p.Ticket)
                            .ThenInclude(t => t.Department)
                        .Include(p => p.Ticket)
                            .ThenInclude(t => t.DepartmentService)
                        .Include(m => m.AppUser)
                        .AsNoTracking()
                        .Where(m => m.AppUserId == userId)
                       .Select(user => new TicketUserDto
                       {
                           Id = user.Id,
                           AppUserId = user.AppUserId,
                           AppUsername = user.AppUser.Username,
                           TicketId = user.TicketId,
                           AssignedOn = user.CreatedOn,
                           Ticket = new TicketListDto
                           {
                               Id = user.Ticket.Id,
                               AppUserId = user.Ticket.AppUserId,
                               Subject = user.Ticket.Subject,
                               DepartmentId = user.Ticket.DepartmentId,
                               Department = user.Ticket.Department.Name,
                               DepartmentServiceId = user.Ticket.DepartmentServiceId,
                               DepartmentService = user.Ticket.DepartmentService == null ? null : user.Ticket.DepartmentService.Name,
                               Email = user.Ticket.Email,
                               FullName = user.Ticket.FullName,
                               PriorityId = user.Ticket.PriorityId,
                               Priority = user.Ticket.PriorityId.ToNameString(),
                               StatusId = user.Ticket.StatusId,
                               Status = user.Ticket.StatusId.ToNameString(),
                               CreatedOn = user.Ticket.CreatedOn,
                               TicketNumber = user.Ticket.TicketNumber,
                               LastRepliedOn = user.Ticket.LastRepliedOn,
                               LastReplier = user.Ticket.LastReplier,
                           }
                       }).ToListAsync();

        }



        public async Task<bool> RemoveTicketUser(int ticketId, int appuserId)
        {
            if (!_currentUserService.HavePermission(Permissions.ManageAssigneeForTickets))
                throw new ForbiddenException();


            var assignee = await _context.TicketUsers
                .Include(t => t.Ticket)
                .Include(t => t.AppUser)
                .FirstOrDefaultAsync(p => p.TicketId == ticketId && p.AppUserId == appuserId);

            if (assignee == null)
                throw new NotFoundException("Assignee not found");


         

            assignee.IsDeleted = true;

            //_emailService.RemovedFromTicket(new EmailTemplates.ViewModels.Tickets.RemovedFromTicketEmailViewModel
            //(
            //    username: assignee.AppUser.Username,
            //    ticketSubject: assignee.Ticket?.Subject,
            //    toEmailId: assignee.AppUser.Email
            //));
            return (await _context.SaveChangesAsync()) > 0;

        }

        #endregion

    }
}
