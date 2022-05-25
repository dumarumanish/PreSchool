using PreSchool.Application.Events;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Application.Services.AppUsers;
using PreSchool.Application.Services.Contacts.Events;
using PreSchool.Application.Services.Contacts.Models.Commands;
using PreSchool.Application.Services.Contacts.Models.Dtos;
using PreSchool.Application.Services.Contacts.Models.Queries;
using PreSchool.Application.Services.Emails;
using PreSchool.Application.Services.Files;
using PreSchool.Data;
using PreSchool.Data.Entities.Contacts;
using PreSchool.EmailTemplates.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly IHostingEnvironmentService _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtService _jwtService;
        private readonly AppSettings _appSettings;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly IEventPublisher _eventPublisher;

        public ContactService(
           IApplicationDbContext context,
            IDateTime dateTime,
            IHostingEnvironmentService hostingEnvironment,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor,
            IJwtService jwtService,
            IFileService fileService,
            IEmailService emailService,
           IEventPublisher eventPublisher

            )
        {
            _context = context;
            _dateTime = dateTime;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _jwtService = jwtService;
            _appSettings = appSettings.Value;
            _fileService = fileService;
            _emailService = emailService;
            _eventPublisher = eventPublisher;


        }

        #region Contact

        public async Task<int> InsertUpdateContact(InsertUpdateContact contact)
        {
            if (contact.Id == 0)
            {
                var newContact = new Contact
                {
                    CreatedBy = 0,
                    CreatedOn = _dateTime.Now,
                    IssueTypeId = contact.IssueTypeId,
                    Email = contact.Email,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    IsAddressed = false,
                    Message = contact.Message,
                    MobileNumber = contact.MobileNumber,
                    Subject = contact.Subject,

                };
                _context.Contacts.Add(newContact);

                await _context.SaveChangesAsync();
                ////send email to client.
                //var contactUsEmailVM = new ContactUsEmailViewModel(
                //                        newContact.FirstName,
                //                        newContact.Email
                //                        );

                //_emailService.ContactUs(contactUsEmailVM);
                ////send email to support.
                ////send email.
                //var issueType = await _context.IssueTypes
                //             .FirstOrDefaultAsync(u => u.Id == newContact.IssueTypeId);
                //var issueTypeName = issueType.Name;
                //var supportSontactUsEmailVM = new SupportContactUsEmailViewModel(
                //                        issueTypeName,
                //                        newContact.FirstName,
                //                        newContact.LastName,
                //                        newContact.Email,
                //                        newContact.MobileNumber,
                //                        newContact.Message,
                //                        _appSettings.SupportContactUsEmail
                //                        );

                //_emailService.SupportContactUs(supportSontactUsEmailVM);
                ////notification.
                _eventPublisher.Publish(new ContactUsEvent(contact.FirstName, contact.Email));

                return newContact.Id;
            }
            else
            {
                var existing = await _context.Contacts.FindAsync(contact.Id);
                if (existing == null)
                    throw new BadRequestException("Invalid Contact", "Invalid Contact Id");
                existing.IssueTypeId = contact.IssueTypeId;
                existing.Email = contact.Email;
                existing.FirstName = contact.FirstName;
                existing.LastName = contact.LastName;
                existing.IsAddressed = false;
                existing.Message = contact.Message;
                existing.MobileNumber = contact.MobileNumber;
                existing.Subject = contact.Subject;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<ContactDto>> GetContactList(ContactFilter filter)
        {

            var contact = _context.Contacts
                .Include(f => f.IssueType)
          .AsNoTracking()
          .Where(e => e.IsDeleted != true)
          .AsQueryable();

            if (filter != null)
            {

                if (filter.IssueTypeId.HasValue)
                    contact = contact.Where(l => l.IssueTypeId == filter.IssueTypeId);

            }

            return await contact
                     .Select(d => new ContactDto

                     {
                         Id = d.Id,
                         IssueName = d.IssueType.Name,
                         IssueTypeId = d.IssueTypeId,
                         Email = d.Email,
                         FirstName = d.FirstName,
                         LastName = d.LastName,
                         IsAddressed = d.IsAddressed,
                         Message = d.Message,
                         MobileNumber = d.MobileNumber,
                         Subject = d.Subject,
                         RepliedSubject = d.RepliedSubject,
                         RepliedMessage = d.RepliedMessage,
                         ResponseDate = d.ResponseDate,

                     }).ToListAsync();
        }

        public async Task<bool> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                throw new BadRequestException("Invalid contact", "contact not found. ");

            contact.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<ContactDto> GetContact(int id)
        {
            var contact = await _context.Contacts
                .Include(n => n.IssueType)
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (contact == null)
                throw new NotFoundException("Contact not found");
            return new ContactDto
            {
                Id = contact.Id,
                IssueName = contact.IssueType.Name,
                IssueTypeId = contact.IssueTypeId,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                IsAddressed = contact.IsAddressed,
                Message = contact.Message,
                MobileNumber = contact.MobileNumber,
                Subject = contact.Subject,
                RepliedSubject = contact.RepliedSubject,
                RepliedMessage = contact.RepliedMessage,
                ResponseDate = contact.ResponseDate,
            };
        }


        public async Task<List<ContactDto>> GetContactbyIssueType(int issueTypeId)
        {
            return await _context.Contacts
                .Include(n => n.IssueType)
                .OrderByDescending(f => f.CreatedOn)
                .Where(c => c.IssueTypeId == issueTypeId)
                 .Select(m => new ContactDto
                 {
                     Id = m.Id,
                     IssueName = m.IssueType.Name,
                     IssueTypeId = m.IssueTypeId,
                     Email = m.Email,
                     FirstName = m.FirstName,
                     LastName = m.LastName,
                     IsAddressed = m.IsAddressed,
                     Message = m.Message,
                     MobileNumber = m.MobileNumber,
                     Subject = m.Subject,
                     RepliedSubject =m.RepliedSubject,
                     RepliedMessage =m.RepliedMessage,
                     ResponseDate =m.ResponseDate,

                 }).ToListAsync();

        }


        public async Task<bool> AddressContact(AddressContact reply)
        {

            var contact = await _context.Contacts
                .Include(n => n.IssueType)
                .FirstOrDefaultAsync(n => n.Id == reply.ContactId);

            if (contact == null)
                throw new NotFoundException("Contact not found");

            contact.IsAddressed = true;
            contact.RepliedSubject = reply.Subject;
            contact.RepliedMessage = reply.Message;
            contact.ResponseDate = _dateTime.Now;
            var res = (await _context.SaveChangesAsync()) > 0;

            //// Send reply to sender
            ////if (res)
            ////    _emailSender.SendEmailAsync(contact.Email, reply.Subject, reply.Message);
            //var contactEmailVM = new AddressContactEmailViewModel(
            //    reply.Message,
            //     contact.Email,
            //    reply.Subject
            //               );
            //_emailService.AddressContact(contactEmailVM);

            //_eventPublisher.Publish(new ContactResponseEvent(contact));

            return res;
        }
        #endregion

        #region IssueType

        public async Task<int> InsertUpdateIssueType(InsertUpdateIssueType issueType)
        {
            if (issueType.Id == 0)
            {

                var type = new IssueType
                {
                    Description = issueType.Description,
                    Name = issueType.Name,
                    SortOrder = issueType.SortOrder,

                };
                _context.IssueTypes.Add(type);
                await _context.SaveChangesAsync();
                return type.Id;
            }
            else
            {
                var existingType = await _context.IssueTypes.FindAsync(issueType.Id);
                if (existingType == null)
                    throw new BadRequestException("Invalid issueType", "Invalid issueType Id");
                existingType.Name = issueType.Name;
                existingType.Description = issueType.Description;
                existingType.SortOrder = issueType.SortOrder;

                if (await _context.SaveChangesAsync() > 0)
                    return existingType.Id;
            }
            return 0;
        }

        public async Task<List<IssueTypeDto>> GetIssueTypeList()
        {
            return await _context.IssueTypes.OrderBy(i => i.SortOrder).Select(m => new IssueTypeDto
            {
                Id = m.Id,
                Description = m.Description,
                Name = m.Name,
                SortOrder = m.SortOrder,

            }).ToListAsync();
        }
        public async Task<IssueTypeDto> GetIssueTypeById(int id)
        {
            var issueType = await _context.IssueTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (issueType == null)
                throw new NotFoundException("issueType not found");
            return new IssueTypeDto
            {
                Id = issueType.Id,
                Description = issueType.Description,
                Name = issueType.Name,
                SortOrder = issueType.SortOrder,

            };
        }
        public async Task<bool> DeleteIssueType(int id)
        {
            var issueType = await _context.IssueTypes.FindAsync(id);
            if (issueType == null)
                throw new BadRequestException("Invalid issueType", "issuetype not found.");

            var contact = await _context.Contacts.FirstOrDefaultAsync(f => f.IssueTypeId == id);

            if (contact != null)
                throw new BadRequestException("Invalid", "issue type is used on contact");

            issueType.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

    }
}

