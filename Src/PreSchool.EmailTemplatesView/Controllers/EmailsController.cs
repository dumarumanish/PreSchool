using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PreSchool.EmailTemplates;
using PreSchool.EmailTemplates.ViewModels;

namespace PreSchool.EmailTemplatesView.Controllers
{
    public class EmailsController : Controller
    {
        public IActionResult Index(string template)
        {
            switch (template)
            {
                case nameof(Templates.HtmlEmail):
                    return View(Templates.HtmlEmail, new EmailViewModel()
                    {
                        Body = "This is body",
                        Title = "This is title"
                    });

                case nameof(Templates.HelloWorld):
                    return View(Templates.HelloWorld, new HelloWorldViewModel("https://google.com"));

                //case nameof(Templates.ReachToUs):
                //    return View(Templates.ReachToUs, new ReachToUsViewModel()
                //    {
                //        Message = "This is Message",
                //    });

                case nameof(Templates.TextEmail):
                    return View(Templates.TextEmail, new EmailViewModel()
                    {
                        Body = "This is body",
                        Title = "This is title"
                    });


                case nameof(Templates.WelcomeCustomer):
                    return View(Templates.WelcomeCustomer, new WelcomeCustomerEmailViewModel("User","1452", "user@email.com"));


                case nameof(Templates.WelcomeInternalUser):
                    return View(Templates.WelcomeInternalUser, new WelcomeInternalUserEmailViewModel("User", "user@email.com"));





                case nameof(Templates.UserStatusChange):
                    return View(Templates.UserStatusChange, new UserStatusChangeEmailViewModel("User", "Deactive", "Remark", ""));

                case nameof(Templates.UserActiveStatus):
                    return View(Templates.UserActiveStatus, new UserActiveStatusEmailViewModel("User", "Remark", "")
                   );


                case nameof(Templates.FeedbackAcknowledge):
                    return View(Templates.FeedbackAcknowledge, new FeedbackAcknowledgeEmailViewModel("User", "")
                   );

                case nameof(Templates.FeedbackResponse):
                    return View(Templates.FeedbackResponse, new FeedbackResponseEmailViewModel("User", "2020/1/1", "123", "")
                   );

                case nameof(Templates.ForgotPassword):
                    return View(Templates.ForgotPassword, new ForgotPasswordEmailViewModel("User", "123", "")
                   );


                case nameof(Templates.ResetPassword):
                    return View(Templates.ResetPassword, new ResetPasswordEmailViewModel("User", "")
                    );

                case nameof(Templates.BlogPublished):
                    return View(Templates.BlogPublished, new BlogPublishedEmailViewModel("User", "Title", "Url", "")
                   );




                #region Subscriptions

                case nameof(Templates.Subscribed):
                    return View(Templates.Subscribed, new SubscribedEmailViewModel(
                        "User", "Email"));

                #endregion

        
                default:

                    ViewData["Templates"] = GetAllTemplates();

                    return View();
            }
        }


        public static List<string> GetAllTemplates()
        {
            return typeof(Templates)
               .GetFields(BindingFlags.Public | BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(x => x.Name)
                .ToList();
        }

    }
}