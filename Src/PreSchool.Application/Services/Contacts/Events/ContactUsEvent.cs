using PreSchool.Data.Entities.AppUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Contacts.Events
{
    public class ContactUsEvent
    {
        public ContactUsEvent(string userName,string email)
        {

            UserName = userName;
            Email = email;
        }

        public string UserName { get; }
        public string Email { get; }
    }
}
