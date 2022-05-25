using PreSchool.Data.Entities;
using PreSchool.Data.Entities.AppConfigurations;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.Common.Addresses;
using PreSchool.Data.Entities.Contacts;
using PreSchool.Data.Entities.Departments;
using PreSchool.Data.Entities.Emails;
using PreSchool.Data.Entities.Files;
using PreSchool.Data.Entities.Notifications;
using PreSchool.Data.Entities.Payments;
using PreSchool.Data.Entities.Seo;
using PreSchool.Data.Entities.Settings;
using PreSchool.Data.Entities.Stores;
using PreSchool.Data.Entities.Taxes;
using PreSchool.Data.Entities.Tickets;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PreSchool.Data.Entities.Schools;
using PreSchool.Data.Entities.FeeSettings;
using PreSchool.Data.Entities.Students;

namespace PreSchool.Application.Infastructures
{
    public interface IApplicationDbContext
    {


     

        #region AppConfigurations

        DbSet<AppFeature> AppFeatures { get; set; }

        DbSet<AppFeatureGroup> AppFeatureGroups { get; set; }

        DbSet<Permission> Permissions { get; set; }
        DbSet<PermissionGroup> PermissionGroups { get; set; }


        #endregion

        #region Stores

        DbSet<Store> Stores { get; set; }

        DbSet<StoreFeature> StoreFeatures { get; set; }

        DbSet<StoreMapping> StoreMappings { get; set; }

        DbSet<StoreAddress> StoreAddresss { get; set; }

        DbSet<StoreCollectionCenter> StoreCollectionCenters { get; set; }

        DbSet<StoreImageType> StoreImageTypes { get; set; }

        DbSet<StoreImage> StoreImages { get; set; }


        DbSet<StoreSocialMedia> StoreSocialMedias { get; set; }
        DbSet<StoreUser> StoreUsers { get; set; }

        #endregion

        #region school

        DbSet<School> Schools { get; set; }
        DbSet<Branch> Branches { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<Batch> Batches { get; set; }
        DbSet<Grade> Grades { get; set; }

        #endregion

        #region students

        DbSet<StudentEnquiry> StudentEnquiries { get; set; }
        DbSet<StudentRegistration> StudentRegistrations { get; set; }
        DbSet<Fee> Fees { get; set; }
        DbSet<AdditionalFee> AdditionalFees { get; set; }
        DbSet<FeePayment> FeePayments { get; set; }
        DbSet<BadDept> BadDepts { get; set; }

        #endregion

        #region Fee Settings

        DbSet<BillPeriod> BillPeriods { get; set; }
        DbSet<FeeGroup> FeeGroups { get; set; }
        DbSet<FeeRate> FeeRates { get; set; }
        DbSet<FeeTitle> FeeTitles { get; set; }
        DbSet<Month> Months { get; set; }
        DbSet<StudentFee> StudentFees { get; set; }
        DbSet<Discount> Discounts { get; set; }
        DbSet<StudentDiscount> StudentDiscounts { get; set; }
        DbSet<OpeningBalance> OpeningBalances { get; set; }



        #endregion

        #region AppUsers
        DbSet<AppUser> AppUsers { get; set; }
        DbSet<AppUserPassword> AppUserPasswords { get; set; }
        DbSet<AppUserRole> AppUserRoles { get; set; }
        DbSet<AppUserType> AppUserTypes { get; set; }

        DbSet<AppUserLoginHistory> AppUserLoginHistories { get; set; }
        DbSet<AppUserOtpCode> AppUserOtpCodes { get; set; }


        DbSet<Role> Roles { get; set; }
        DbSet<RolePermission> RolePermissions { get; set; }

        DbSet<InternalUserAcademic> InternalUserAcademics { get; set; }
        DbSet<InternalUserDocuments> InternalUserDocuments { get; set; }
        DbSet<InternalUserEmergencyContact> InternalUserEmergencyContacts { get; set; }
        DbSet<InternalUserExperience> InternalUserExperiences { get; set; }

        #endregion


        #region Files

        DbSet<File> Files { get; set; }


        #endregion

        #region Emails

        DbSet<EmailHistory> EmailHistories { get; set; }

        #endregion

        #region Seo

        DbSet<SeoSetting> SeoSettings { get; set; }

        DbSet<SeoUrl> UrlRecords { get; set; }

        #endregion

        #region Bank

        DbSet<Bank> Banks { get; set; }

        #endregion

        #region Addresses

        DbSet<Address> Addresses { get; set; }

        DbSet<Country> Countries { get; set; }

        DbSet<Province> Provinces { get; set; }

        DbSet<City> Cities { get; set; }
        DbSet<Region> Regions { get; set; }
        #endregion

        #region Tax

        DbSet<TaxCategory> TaxCategories { get; set; }

        DbSet<TaxSetting> TaxSettings { get; set; }


        #endregion

     
        #region Payments

        DbSet<PaymentMode> PaymentModes { get; set; }

        #endregion

        DbSet<Log> Logs { get; set; }


        #region Ticket
        DbSet<Ticket> Tickets { get; set; }
        DbSet<TicketAttachment> TicketAttachments { get; set; }
        DbSet<TicketReply> TicketReplies { get; set; }
        DbSet<TicketReplyAttachment> TicketReplyAttachments { get; set; }
        DbSet<TicketUser> TicketUsers { get; set; }

        #endregion

        #region Department

        DbSet<Department> Departments { get; set; }
        DbSet<DepartmentService> DepartmentServices { get; set; }

        #endregion

        #region Contacts

        DbSet<Contact> Contacts { get; set; }
        DbSet<IssueType> IssueTypes { get; set; }

        #endregion

        #region Notification

        DbSet<NotificationActivityType> NotificationActivityTypes { get; set; }

        DbSet<Notification> Notifications { get; set; }

        DbSet<NotificationGroup> NotificationGroups { get; set; }


        DbSet<NotificationGroupSubscribedActivity> NotificationGroupSubscribedActivities { get; set; }


        DbSet<NotificationGroupSubscriber> NotificationGroupSubscribers { get; set; }


        #endregion

        Task<int> SaveChangesAsync();
    }
}
