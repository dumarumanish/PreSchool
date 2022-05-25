using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.Notifications
{
    public enum NotificationActivityTypeEnum
    {
        [Display(GroupName = "None", Name = "None", Description = "Default notification", ShortName = "")]
        None = 1,


        #region Customers

        [Display(GroupName = "Customer", Name = "Customer registered", Description = "Customer registered", ShortName = "/u/customer-mgmt/detail/")]
        CustomerRegistered,

        [Display(GroupName = "Customer", Name = "Customer deleted", Description = "Customer deleted", ShortName = "")]
        CustomerDeleted,

        [Display(GroupName = "Customer", Name = "Customer details updated", Description = "Customer details updated", ShortName = "/u/customer-mgmt/detail/")]
        CustomerDetailUpdated,

        [Display(GroupName = "Customer", Name = "Customer document added", Description = "Customer document added", ShortName = "/u/customer-mgmt/detail/")]
        CustomerDocumentAdded,

        [Display(GroupName = "Customer", Name = "Customer email verified", Description = "Customer email verified", ShortName = "/u/customer-mgmt/detail/")]
        CustomerEmailVerified,


        #endregion

        #region Catlogs
        [Display(GroupName = "Product", Name = "Product submited", Description = "Product submited", ShortName = "/u/product-mgmt/detail/")]
        ProductSubmited,

        [Display(GroupName = "Product", Name = "Product approval status changed", Description = "Product approval status changed", ShortName = "/u/product-mgmt/detail/")]
        ProductApprovalStatusChanged,
        #endregion

        #region Vendors
        [Display(GroupName = "Vendor", Name = "Vendor Registered", Description = "Vendor Registered", ShortName = "/u/vendor-mgmt/detail/")]
        VendorRegistered,

        [Display(GroupName = "Vendor", Name = "Vendor Status Changed", Description = "Vendor Status Changed", ShortName = "/u/vendor-mgmt/detail/")]
        VendorStatusChanged,

        [Display(GroupName = "Vendor", Name = "Vendor Verified", Description = "Vendor Verified", ShortName = "/u/vendor-mgmt/detail/")]
        VendorVerified,

        [Display(GroupName = "Vendor", Name = "Vendor Email Verified", Description = "Vendor Email Verified", ShortName = "/u/vendor-mgmt/detail/")]
        VendorEmailVerified,

        #endregion

        #region Orders
        [Display(GroupName = "Order", Name = "Order Cancelled", Description = "Order Cancelled", ShortName = "/u/order-mgmt/detail/")]
        OrderCancelled,

        [Display(GroupName = "Order", Name = "Order Cancelled By Vendor", Description = "Order Cancelled By Vendor", ShortName = "/u/order-mgmt/detail/")]
        OrderCancelledByVendor,

        [Display(GroupName = "Order", Name = "Order Deffered", Description = "Order Deffered", ShortName = "/u/order-mgmt/detail/")]
        OrderDeffered,

        [Display(GroupName = "Order", Name = "Order CheckOut", Description = "Order CheckOut", ShortName = "/u/order-mgmt/detail/")]
        OrderCheckOut,

        [Display(GroupName = "Order", Name = "Order Ready For Collection", Description = "Order Ready For Collection", ShortName = "/u/order-mgmt/detail/")]
        OrderReadyForCollection,

        [Display(GroupName = "Order", Name = "Order Shipped", Description = "Order Shipped", ShortName = "/u/order-mgmt/detail/")]
        OrderShipped,

        [Display(GroupName = "Order", Name = "Order Delivered", Description = "Order Delivered", ShortName = "/u/order-mgmt/detail/")]
        OrderDelivered,

        [Display(GroupName = "Order", Name = "Order Partially Completed", Description = "Order Partially Completed", ShortName = "/u/order-mgmt/detail/")]
        OrderPartiallyCompleted,

        [Display(GroupName = "Order", Name = "Order Delivery Attempt Failed", Description = "Order Delivery Attempt Failed", ShortName = "/u/order-mgmt/detail/")]
        OrderDeliveryAttemptFailed,

        [Display(GroupName = "Order", Name = "Order Delivery Attempt Failed Second", Description = "Order Delivery Attempt Failed Second", ShortName = "/u/order-mgmt/detail/")]
        OrderDeliveryAttemptFailedSecond,

        [Display(GroupName = "Order", Name = "Order Conformed", Description = "Order Conformed", ShortName = "/u/order-mgmt/detail/")]
        OrderConformed,
        #endregion

        #region Conntact Us
        [Display(GroupName = "ContactUs", Name = "ContactUs", Description = "ContactUs", ShortName = "/u/contactus-mgmt/detail/")]
        ContactUs,
        #endregion



    }
}
