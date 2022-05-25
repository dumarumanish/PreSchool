using PreSchool.Application.Infastructures;
using PreSchool.Data.Entities;
using PreSchool.Data.Entities.AppConfigurations;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.AppUsers.Configurations;
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
using System.Linq;
using System.Threading.Tasks;
using PreSchool.Data.Entities.Schools;
using PreSchool.Data.Entities.FeeSettings;
using PreSchool.Data.Entities.Students;

namespace PreSchool.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;


        public ApplicationDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        //{
        //}

        #region AppConfigurations

        public DbSet<AppFeature> AppFeatures { get; set; }

        public DbSet<AppFeatureGroup> AppFeatureGroups { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }


        #endregion

        #region Stores

        public DbSet<Store> Stores { get; set; }

        public DbSet<StoreFeature> StoreFeatures { get; set; }


        public DbSet<StoreAddress> StoreAddresss { get; set; }

        public DbSet<StoreCollectionCenter> StoreCollectionCenters { get; set; }

        public DbSet<StoreImageType> StoreImageTypes { get; set; }


        public DbSet<StoreSocialMedia> StoreSocialMedias { get; set; }

        public DbSet<StoreMapping> StoreMappings { get; set; }

        public DbSet<StoreImage> StoreImages { get; set; }


        public DbSet<StoreUser> StoreUsers { get; set; }

        #endregion

        #region school

        public DbSet<School> Schools { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Grade> Grades { get; set; }


        #endregion

        #region students

        public DbSet<StudentEnquiry> StudentEnquiries { get; set; }
        public DbSet<StudentRegistration> StudentRegistrations { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<AdditionalFee> AdditionalFees { get; set; }
        public DbSet<FeePayment> FeePayments { get; set; }
        public DbSet<BadDept> BadDepts { get; set; }

        #endregion

        #region Fee Settings

        public DbSet<BillPeriod> BillPeriods { get; set; }
        public DbSet<FeeGroup> FeeGroups { get; set; }
        public DbSet<FeeRate> FeeRates { get; set; }
        public DbSet<FeeTitle> FeeTitles { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<StudentFee> StudentFees { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<StudentDiscount> StudentDiscounts { get; set; }
        public DbSet<OpeningBalance> OpeningBalances { get; set; }



        #endregion

        #region AppUsers
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserPassword> AppUserPasswords { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<AppUserType> AppUserTypes { get; set; }


        public DbSet<AppUserLoginHistory> AppUserLoginHistories { get; set; }
        public DbSet<AppUserOtpCode> AppUserOtpCodes { get; set; }


        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }


        public DbSet<InternalUserAcademic> InternalUserAcademics { get; set; }
        public DbSet<InternalUserDocuments> InternalUserDocuments { get; set; }
        public DbSet<InternalUserEmergencyContact> InternalUserEmergencyContacts { get; set; }
        public DbSet<InternalUserExperience> InternalUserExperiences { get; set; }



        #endregion

        #region Files

        public DbSet<File> Files { get; set; }


        #endregion

        #region Emails

        public DbSet<EmailHistory> EmailHistories { get; set; }

        #endregion

        #region Seo

        public DbSet<SeoSetting> SeoSettings { get; set; }

        public DbSet<SeoUrl> UrlRecords { get; set; }

        #endregion

        #region Bank

        public DbSet<Bank> Banks { get; set; }

        #endregion

        #region Addresses


        public DbSet<Address> Addresses { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<Region> Regions { get; set; }


        #endregion

        #region Tax
        public DbSet<TaxCategory> TaxCategories { get; set; }

        public DbSet<TaxSetting> TaxSettings { get; set; }
        #endregion

        #region Payments

        public DbSet<PaymentMode> PaymentModes { get; set; }

        #endregion

        public DbSet<Log> Logs { get; set; }

        #region Ticket
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<TicketReply> TicketReplies { get; set; }
        public DbSet<TicketReplyAttachment> TicketReplyAttachments { get; set; }
        public DbSet<TicketUser> TicketUsers { get; set; }

        #endregion

        #region Department

        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentService> DepartmentServices { get; set; }


        #endregion

        #region Contacts

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<IssueType> IssueTypes { get; set; }


        #endregion

        #region Notification

        public DbSet<NotificationActivityType> NotificationActivityTypes { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<NotificationGroup> NotificationGroups { get; set; }


        public DbSet<NotificationGroupSubscribedActivity> NotificationGroupSubscribedActivities { get; set; }


        public DbSet<NotificationGroupSubscriber> NotificationGroupSubscribers { get; set; }


        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.ApplyAllConfiguration();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);


            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                // EF Core 1 & 2
                //   property.Relational().ColumnType = "decimal(18, 6)";

                // EF Core 3
                property.SetColumnType("decimal(18, 2 )");

                // EF Core 5
                //property.SetPrecision(18);
                //property.SetScale(6);
            }

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            // .UseLazyLoadingProxies()
            ;
        }

        public Task<int> SaveChangesAsync()
        {
            foreach (var entry in ChangeTracker.Entries<CommonProperties>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.IsAuthenticated ? _currentUserService.AppUserId : 0;
                        entry.Entity.CreatedOn = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = _currentUserService.IsAuthenticated ? _currentUserService.AppUserId : 0;
                        entry.Entity.ModifiedOn = _dateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync();
        }
    }
}
