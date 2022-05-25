using PreSchool.Application.Services.Contacts.Models.Commands;
using PreSchool.Application.Services.Contacts.Models.Dtos;
using PreSchool.Application.Services.Contacts.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Contacts
{
    public interface IContactService
    {
        Task<bool> AddressContact(AddressContact reply);
        Task<bool> DeleteContact(int id);
        Task<bool> DeleteIssueType(int id);
        Task<ContactDto> GetContact(int id);
        Task<List<ContactDto>> GetContactbyIssueType(int issueTypeId);
        Task<List<ContactDto>> GetContactList(ContactFilter filter);
        Task<IssueTypeDto> GetIssueTypeById(int id);
        Task<List<IssueTypeDto>> GetIssueTypeList();
        Task<int> InsertUpdateContact(InsertUpdateContact contact);
        Task<int> InsertUpdateIssueType(InsertUpdateIssueType issueType);
    }
}