using System.Collections.Generic;

namespace PreSchool.Application.Services.AppUsers.Models.Dtos
{
    public class ValidUserDto
    {
        public bool IsLoginValid { get; set; }
        public string Token { get; set; }
        public bool RequiredPasswordChange { get; set; }
        public string Remarks { get; set; }
        public int? StoreId { get; set; }
        public int AppUserTypeId { get; set; }
        public string FullName { get; set; }
        public bool IsEmailVerified { get; set; }
        public string EmailAddress { get; set; }
        public bool IsPhoneVerified { get; set; }
        public string PhoneNumber { get; set; }

    }
}
