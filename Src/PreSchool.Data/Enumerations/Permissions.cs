using PreSchool.Data.Attributes;
using PreSchool.Data.Entities.AppUsers;
using System.ComponentModel.DataAnnotations;

namespace PreSchool.Data.Enumerations
{
    public enum Permissions
    {

        #region Internal Application Management

        [Display(GroupName = nameof(PermissionGroups.InternalAppManagement), Name = "TotalAccess", Description = "Total access of the application")]
        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        TotalAccess = 1,

        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        [Display(GroupName = nameof(PermissionGroups.InternalAppManagement), Description = "Create internal users")]
        CreateInternalUser,

        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        [Display(GroupName = nameof(PermissionGroups.InternalAppManagement), Description = "View internal users")]
        ReadInternalUser,

        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        [Display(GroupName = nameof(PermissionGroups.InternalAppManagement), Description = "Update users")]
        UpdateInternalUser,

        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        [Display(GroupName = nameof(PermissionGroups.InternalAppManagement), Description = "Delete users")]
        DeleteInternalUser,




        #endregion

        #region Setting management
        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        [Display(GroupName = nameof(PermissionGroups.SettingManagement), Description = "Seo settings")]
        ManageSeoSettings,

        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        [Display(GroupName = nameof(PermissionGroups.SettingManagement), Description = "Manage banks ")]
        ManageBanks,
        #endregion

        #region RoleMangement

        [Display(GroupName = nameof(PermissionGroups.RoleManagement), Description = "Manage roles")]
        RoleManagement,


        #endregion

        #region DepartmentMangement

        [Display(GroupName = nameof(PermissionGroups.DepartmentManagement), Description = "Manage store departments")]
        StoreDepartmentManagement,

        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        [Display(GroupName = nameof(PermissionGroups.DepartmentManagement), Description = "Manage internal user departments")]
        InternalDepartmentManagement,

        [Display(GroupName = nameof(PermissionGroups.DepartmentManagement), Description = "Manage vendor departments")]
        VendorDepartmentManagement,
        #endregion

        #region AppFeature Management

        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        [Display(GroupName = nameof(PermissionGroups.AppFeatureManagement), Description = "View all app features")]
        ViewAllAppFeatures,

        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        [Display(GroupName = nameof(PermissionGroups.AppFeatureManagement), Description = "View store app features")]
        ViewStoreAppFeatures,

        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        [Display(GroupName = nameof(PermissionGroups.AppFeatureManagement), Description = "Manage feature groups")]
        ManageAppFeatureGroups,

        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        [Display(GroupName = nameof(PermissionGroups.AppFeatureManagement), Description = "Manage store app features")]
        ManageStoreAppFeatures,

        #endregion

        #region Address management
        [Display(GroupName = nameof(PermissionGroups.AddressManagement), Description = "Manage addresses")]
        ManageAddresses,

        #endregion

        #region Tax

        [Display(GroupName = nameof(PermissionGroups.TaxManagement), Description = "Manage tax settings")]
        ManageTaxSettings,

        [Display(GroupName = nameof(PermissionGroups.TaxManagement), Description = "Add tax categories")]
        AddTaxCategories,

        [Display(GroupName = nameof(PermissionGroups.TaxManagement), Description = "Update tax categories")]
        UpdateTaxCategories,

        [Display(GroupName = nameof(PermissionGroups.TaxManagement), Description = "Delete tax categories")]
        DeleteTaxCategories,
        #endregion

        #region Payments
        [Display(GroupName = nameof(PermissionGroups.PaymentManagement), Description = "Manage payment mode")]
        ManagePaymentMode,
        #endregion

        #region Ticket Management



        [Display(GroupName = nameof(PermissionGroups.TicketManagement), Description = "View tickets")]
        ViewTickets,


        [Display(GroupName = nameof(PermissionGroups.TicketManagement), Description = "Add tickets")]
        AddTickets,

        [Display(GroupName = nameof(PermissionGroups.TicketManagement), Description = "Manage assignee for tickets")]
        ManageAssigneeForTickets,

        [Display(GroupName = nameof(PermissionGroups.TicketManagement), Description = "View assigned user's tickets")]
        ViewAssignedUsersTickets,

        [Display(GroupName = nameof(PermissionGroups.TicketManagement), Description = "Reply tickets")]
        ReplyTickets,

        [Display(GroupName = nameof(PermissionGroups.TicketManagement), Description = "Delete tickets reply")]
        DeleteTicketsReply,

        [Display(GroupName = nameof(PermissionGroups.TicketManagement), Description = "Change tickets status")]
        ChangeTicketsStatus,

        [Display(GroupName = nameof(PermissionGroups.TicketManagement), Description = "Update tickets")]
        UpdateTickets,

        [Display(GroupName = nameof(PermissionGroups.TicketManagement), Description = "Delete tickets")]
        DeleteTickets,

        #endregion

        #region ContactManagement

        [Display(GroupName = nameof(PermissionGroups.ContactManagement), Description = "Can create contacts")]
        CreateContact,

        [Display(GroupName = nameof(PermissionGroups.ContactManagement), Description = "Can view contacts")]
        ReadContact,

        [Display(GroupName = nameof(PermissionGroups.ContactManagement), Description = "Can update contacts")]
        UpdateContact,

        [Display(GroupName = nameof(PermissionGroups.ContactManagement), Description = "Can delete contacts")]
        DeleteContact,

        //issue types.
        [Display(GroupName = nameof(PermissionGroups.ContactManagement), Description = "Can create issue types")]
        CreateIssueType,

        [Display(GroupName = nameof(PermissionGroups.ContactManagement), Description = "Can view issue types")]
        ReadIssueType,

        [Display(GroupName = nameof(PermissionGroups.ContactManagement), Description = "Can update issue types")]
        UpdateIssueType,

        [Display(GroupName = nameof(PermissionGroups.ContactManagement), Description = "Can delete issue types")]
        DeleteIssueType,


        #endregion

        #region Notification Management

        [Display(GroupName = nameof(PermissionGroups.NotificationManagement), Description = "Send notification")]
        SendNotifications,

        [Display(GroupName = nameof(PermissionGroups.NotificationManagement), Description = "View notification")]
        ViewNotifications,


        [Display(GroupName = nameof(PermissionGroups.NotificationManagement), Description = "View deleted notification")]
        ViewDeletedNotifications,

        [Display(GroupName = nameof(PermissionGroups.NotificationManagement), Description = "Delete notification")]
        DeleteNotifications,

        [Display(GroupName = nameof(PermissionGroups.NotificationManagement), Description = "Manage notification groups")]
        ManageNotificationGroups,
        #endregion

        #region Log Management

        [Display(GroupName = nameof(PermissionGroups.LogManagement), Description = "View logs")]
        ViewLogs,

        [Display(GroupName = nameof(PermissionGroups.LogManagement), Description = "View others activity logs")]
        ViewOthersActivityLogs,

        [PermissionLimitedTo(AppUserTypeEnum.Internal)]
        [Display(GroupName = nameof(PermissionGroups.LogManagement), Description = "View error logs")]
        ViewErrorLogs,
        #endregion
    }
}
