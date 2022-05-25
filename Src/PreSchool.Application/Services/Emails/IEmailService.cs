using PreSchool.Application.Services.Emails.Models;
using PreSchool.EmailTemplates.ViewModels;

namespace PreSchool.Application.Services.Emails
{
    public interface IEmailService
    {
        void BlogPublished(BlogPublishedEmailViewModel model);
        void ContactUs(ContactUsEmailViewModel model);
        void CustomerEmailVerifyWelcome(CustomerEmailVerifyWelcomeEmailViewModel model);
        void CustomerVerified(CustomerVerifiedEmailViewModel model);
        void FeedbackAcknowledge(FeedbackAcknowledgeEmailViewModel model);
        void FeedbackResponse(FeedbackResponseEmailViewModel model);
        void ForgotPassword(ForgotPasswordEmailViewModel model);
        void ResendVerificationCode(ResendVerificationCodeEmailViewModel model);
        void ResetPassword(ResetPasswordEmailViewModel model);
        void SendEmail(EmailModel model);
        void Subscribed(SubscribedEmailViewModel model);
        void TicketOpen(TicketOpenEmailViewModel model);
        void TicketReply(TicketReplyEmailViewModel model);
        void UserActiveStatus(UserActiveStatusEmailViewModel model);
        void UserStatusChange(UserStatusChangeEmailViewModel model);
        void WelcomeCustomer(WelcomeCustomerEmailViewModel model);
        void WelcomeCustomerOfSocialMedia(WelcomeCustomerOfSocialMediaEmailViewModel model);
        void WelcomeInternalUser(WelcomeInternalUserEmailViewModel model);
    }
}