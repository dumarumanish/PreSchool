#pragma checksum "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Tickets\TicketOpen.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f44e4474b13027ff1aa96467c915f2b56644f139"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Tickets_TicketOpen), @"mvc.1.0.view", @"/Views/Tickets/TicketOpen.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f44e4474b13027ff1aa96467c915f2b56644f139", @"/Views/Tickets/TicketOpen.cshtml")]
    public class Views_Tickets_TicketOpen : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<eCommerce.EmailTemplates.ViewModels.TicketOpenEmailViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Tickets\TicketOpen.cshtml"
  
    Layout = "_EmailLayoutHtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"content\" style=\"padding: 0 24px 24px;\">\r\n        <div class=\"welcome\"\r\n             style=\"font-size: 20px; font-weight: 700; margin-bottom: 24px; font-family: \'Open Sans\', \'Lato\', sans-serif;\">\r\n            ");
#nullable restore
#line 10 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Tickets\TicketOpen.cshtml"
       Write(Model.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"greeting\" style=\"margin-bottom: 14px;\">\r\n            <span style=\"margin-right: 2px;\">Dear </span>\r\n            <span style=\"font-weight: 700;\">");
#nullable restore
#line 14 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Tickets\TicketOpen.cshtml"
                                       Write(Model.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span>,
        </div>

        <div style=""margin-bottom: 14px;"">
            Thank you for reaching Hattiya with a ticket. Your ticket with the following details has been received.
        </div>

        <div style=""margin-bottom: 14px;"">
            Ticket ID:");
#nullable restore
#line 22 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Tickets\TicketOpen.cshtml"
                 Write(Model.TicketId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Ticket Date:");
#nullable restore
#line 25 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Tickets\TicketOpen.cshtml"
                   Write(Model.TicketDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Details on Ticket:");
#nullable restore
#line 28 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Tickets\TicketOpen.cshtml"
                         Write(Model.Detail);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        </div>
        <div style=""margin-bottom: 14px;"">
            Hattiya service representative will look into your ticket and will reply you within 24-48 hours. Please feel free to reach to us with any additional questions or concerns.
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<eCommerce.EmailTemplates.ViewModels.TicketOpenEmailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
