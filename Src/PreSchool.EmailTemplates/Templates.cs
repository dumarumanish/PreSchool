using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.EmailTemplates
{
    public class Templates
    {
        public const string TextEmail = "/Views/TextEmail.cshtml";
        public const string HtmlEmail = "/Views/HtmlEmail.cshtml";
        public const string HelloWorld = "/Views/HelloWorld/HelloWorldHtml.cshtml";

        #region AppUsers
        public const string WelcomeCustomer = "/Views/Users/WelcomeCustomer.cshtml";
        public const string CustomerVerified = "/Views/Users/CustomerVerified.cshtml";
        public const string WelcomeInternalUser = "/Views/Users/WelcomeInternalUser.cshtml";
        public const string UserStatusChange = "/Views/Users/UserStatusChange.cshtml";
        public const string UserActiveStatus = "/Views/Users/UserActiveStatus.cshtml";
        public const string ForgotPassword = "/Views/Users/ForgotPassword.cshtml";
        public const string ResetPassword = "/Views/Users/ResetPassword.cshtml";
        public const string ResendVerificationCode = "/Views/Users/ResendVerificationCode.cshtml";
        public const string WelcomeCustomerOfSocialMedia = "/Views/Users/WelcomeCustomerOfSocialMedia.cshtml";
        public const string CustomerEmailVerifyWelcome = "/Views/Users/CustomerEmailVerifyWelcome.cshtml";


        #endregion

        #region Bolgs
        public const string BlogPublished = "/Views/Blogs/BlogPublished.cshtml";

        #endregion

        #region FeedBacks
        public const string FeedbackAcknowledge = "/Views/FeedBacks/FeedbackAcknowledge.cshtml";
        public const string FeedbackResponse = "/Views/FeedBacks/FeedbackResponse.cshtml";
        #endregion

        #region Subscriptions

        public const string Subscribed = "/Views/Subscriptions/Subscribed.cshtml";

        #endregion

        #region ContactUs

        public const string ContactUs = "/Views/ContactUs/ContactUs.cshtml";

        #endregion

        #region Tickets

        public const string TicketOpen = "/Views/Tickets/TicketOpen.cshtml";
        public const string TicketReply = "/Views/Tickets/TicketReply.cshtml";


        #endregion
    }
}
