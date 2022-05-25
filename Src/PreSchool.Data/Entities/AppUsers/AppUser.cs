using PreSchool.Data.Entities.Departments;
using PreSchool.Data.Entities.Files;
using PreSchool.Data.Entities.Stores;
using PreSchool.Data.Entities.Students;
using System;
using System.Collections.Generic;

namespace PreSchool.Data.Entities.AppUsers
{

    /// <summary>
    /// Application User
    /// </summary>
    public class AppUser : CommonProperties, ISocialLogin, IStoreMappingSupported
    {
        public string Username { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// Is email verified or not
        /// </summary>
        public bool IsEmailVerified { get; set; }
        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the appUser is required to re-login
        /// </summary>
        public bool RequireReLogin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating number of failed login attempts (wrong password)
        /// </summary>
        public int FailedLoginAttempts { get; set; }

        /// <summary>
        /// Gets or sets the date and time until which a appuser cannot login (locked out)
        /// </summary>
        public DateTime? CannotLoginUntilDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the appuser is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the appuser account is system
        /// </summary>
        public bool IsSystemAccount { get; set; }

        /// <summary>
        /// Gets or sets the appuser system name
        /// </summary>
        public string SystemName { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }
        public bool IsPhoneVerified { get; set; }
        public string JobTitle { get; set; }
        public GenderEnum GenderId { get; set; }
        public DateTime DateOfBirthAD { get; set; }
        public string DateOfBirthBS { get; set; }
        public string BloodGroup { get; set; }
        public string MaritalStatus { get; set; }

        /// <summary>
        /// parent detail.
        /// </summary>
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string GrandFatherName { get; set; }
        public string SpouseName { get; set; }

        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
        /// <summary>
        /// professional information
        /// </summary>
        public int DepartmentId { get; set; }
        public string Position { get; set; }
        public string PermanentPhonenumber { get; set; }
        public string SecondaryPhonenumber { get; set; }
        public EmploymentTypeEnum EmploymentTypeId { get; set; }
        public virtual File Image { get; set; }
        public virtual Department Department { get; set; }



        public virtual ICollection<InternalUserAcademic> InternalUserAcademics { get; set; }
        public virtual ICollection<InternalUserDocuments> InternalUserDocuments { get; set; }
        public virtual ICollection<InternalUserEmergencyContact> InternalUserEmergencyContacts { get; set; }
        public virtual ICollection<InternalUserExperience> InternalUserExperiences { get; set; }

        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
        public virtual ICollection<AppUserPassword> AppUserPasswords { get; set; }
        public virtual ICollection<AppUserLoginHistory> AppUserLoginHistories { get; set; }
        public string FacebookUserId { get; set; }
        public string GoogleUserId { get; set; }

        #region Store User

        /// <summary>
        /// If the appuser is limited to store or not
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Store in which appuser is register
        /// </summary>
        public int? RegisteredInStoreId { get; set; }
        public virtual Store RegisteredInStore { get; set; }

        /// <summary>
        /// If the user is store user,
        /// One appuser may be user of multiple store
        /// </summary>
        public virtual ICollection<StoreUser> StoreUsers { get; set; }

        #endregion
        /// <summary>
        /// Type of app user
        /// </summary>
        public int AppUserTypeId { get; set; }
        public virtual AppUserType AppUserType { get; set; }


        public AppUser()
        {
            InternalUserAcademics = new HashSet<InternalUserAcademic>();
            InternalUserDocuments = new HashSet<InternalUserDocuments>();
            InternalUserEmergencyContacts = new HashSet<InternalUserEmergencyContact>();
            InternalUserExperiences = new HashSet<InternalUserExperience>();
            AppUserRoles = new HashSet<AppUserRole>();
            AppUserPasswords = new HashSet<AppUserPassword>();
            AppUserLoginHistories = new HashSet<AppUserLoginHistory>();
            StoreUsers = new HashSet<StoreUser>();

        }

    }
}
