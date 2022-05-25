using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Services.Contacts;
using PreSchool.Application.Services.Contacts.Models.Commands;
using PreSchool.Application.Services.Contacts.Models.Dtos;
using PreSchool.Application.Services.Contacts.Models.Queries;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [AuthorizeUser]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        #region Contact

        [HttpPost]
        [VerifyReCaptcha]

        public async Task<int> InsertContact(InsertUpdateContact Contact)
        {
            if (Contact == null)
                throw new BadRequestException("Invalid", " Contact is required.");
            Contact.Id = 0;
            return await _contactService.InsertUpdateContact(Contact);
        }

        [HttpPost("{id}/Address")]

        public async Task<bool> AddressContact(int id, AddressContact reply)
        {
            if(id != reply.ContactId)
                throw new BadRequestException("Invalid Id", " Id doesnot match.");
            
            return await _contactService.AddressContact(reply);
        }

        [HttpPut("{id}")]
        public async Task<int> UpdateContact(int id, InsertUpdateContact Contact)
        {
            if (Contact == null)
                throw new BadRequestException("Invalid", "Contact is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id","Id cannot be 0");

            if (id != Contact.Id)
                throw new BadRequestException("Invalid Id", "Id doesnot match");
            return await _contactService.InsertUpdateContact(Contact);

        }

        [HttpGet]
        [AuthorizeUser(Permissions.ReadContact)]

        public async Task<List<ContactDto>> GetContactList([FromQuery] ContactFilter filter)
        {
            return await _contactService.GetContactList(filter);

        }

        [HttpGet("{id}")]
        [AuthorizeUser(Permissions.ReadContact)]

        public async Task<ContactDto> GetContact(int id)
        {
            return await _contactService.GetContact(id);

        }
/// <summary>
/// get contact list by issue type id.
/// </summary>
/// <param name="issueTypeId"></param>
/// <returns></returns>
        [HttpGet("Types/{issueTypeId}")]
        public async Task<List<ContactDto>> GetContactbyIssueType(int issueTypeId)
        {
            return await _contactService.GetContactbyIssueType(issueTypeId);

        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteContact(int id)
        {
            return await _contactService.DeleteContact(id);

        }
        #endregion

        #region IssueType.

        [HttpPost("IssueTypes")]
        [AuthorizeUser(Permissions.CreateIssueType)]

        public async Task<int> InsertIssueType(InsertUpdateIssueType issueType)
        {
            if (issueType == null)
                throw new BadRequestException("Invalid", "Type is required.");
            issueType.Id = 0;
            return await _contactService.InsertUpdateIssueType(issueType);
        }

        [HttpPut("IssueTypes/{id}")]
        [AuthorizeUser(Permissions.UpdateIssueType)]

        public async Task<int> UpdateIssueType(int id, InsertUpdateIssueType issueType)
        {
            if (issueType == null)
                throw new BadRequestException("Invalid", "Type is required.");

            if (id == 0)
                throw new BadRequestException("Invalid Id","id cannot be 0");

            if (id != issueType.Id)
                throw new BadRequestException("Invalid Id", "Id doesnot match");
            return await _contactService.InsertUpdateIssueType(issueType);

        }

        [HttpGet("IssueTypes")]
        public async Task<List<IssueTypeDto>> GetIssueTypeList()
        {
            return await _contactService.GetIssueTypeList();

        }

        [HttpGet("IssueTypes/{id}")]

        public async Task<IssueTypeDto> GetIssueTypeById(int id)
        {
            return await _contactService.GetIssueTypeById(id);

        }
        [HttpDelete("IssueTypes/{id}")]

        [AuthorizeUser(Permissions.DeleteIssueType)]

        public async Task<bool> DeleteIssueType(int id)
        {
            return await _contactService.DeleteIssueType(id);

        }
        #endregion


    }
}