#pragma checksum "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\WelcomeCustomer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "85a24c49d4430db632c9aa5a7d28a214c8d98105"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_WelcomeCustomer), @"mvc.1.0.view", @"/Views/Users/WelcomeCustomer.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"85a24c49d4430db632c9aa5a7d28a214c8d98105", @"/Views/Users/WelcomeCustomer.cshtml")]
    public class Views_Users_WelcomeCustomer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<eCommerce.EmailTemplates.ViewModels.WelcomeCustomerEmailViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\WelcomeCustomer.cshtml"
  
    Layout = "_EmailLayoutHtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"content\" style=\"padding: 0 24px 24px;\">\r\n        <div class=\"welcome\"\r\n             style=\"font-size: 20px; font-weight: 700; margin-bottom: 24px; font-family: \'Open Sans\', \'Lato\', sans-serif;\">\r\n            ");
#nullable restore
#line 10 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\WelcomeCustomer.cshtml"
       Write(Model.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"greeting\" style=\"margin-bottom: 14px;\">\r\n            <span style=\"margin-right: 2px;\">Hello </span>\r\n            <span style=\"font-weight: 700;\">");
#nullable restore
#line 14 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\WelcomeCustomer.cshtml"
                                       Write(Model.Username);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span>,
        </div>

        <div style=""margin-bottom: 14px;"">
            Thank you for registering to Hattiya, Nepal’s unique ecommerce platform.
        </div>
        <div style=""margin-bottom: 14px;"">
            Please insert eight-digit access code ");
#nullable restore
#line 21 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\WelcomeCustomer.cshtml"
                                             Write(Model.OTPCode);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            , for your email verification and login using registered user name and password to the system. Also, we request you not to share your password with any others, for secure browsing.
        </div>
        <div style=""margin-bottom: 14px;"">
            Hattiya would like to welcome once again and look forward for serving you.
        </div>
    </div>





");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<eCommerce.EmailTemplates.ViewModels.WelcomeCustomerEmailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
