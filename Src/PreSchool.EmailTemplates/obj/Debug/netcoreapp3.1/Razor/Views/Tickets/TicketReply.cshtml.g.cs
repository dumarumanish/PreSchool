#pragma checksum "D:\Krenova\Project\PreSchool\Src\PreSchool.EmailTemplates\Views\Tickets\TicketReply.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eb0264444da4893bfcfd19f55e82141b59a2e91f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Tickets_TicketReply), @"mvc.1.0.view", @"/Views/Tickets/TicketReply.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eb0264444da4893bfcfd19f55e82141b59a2e91f", @"/Views/Tickets/TicketReply.cshtml")]
    public class Views_Tickets_TicketReply : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PreSchool.EmailTemplates.ViewModels.TicketReplyEmailViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Krenova\Project\PreSchool\Src\PreSchool.EmailTemplates\Views\Tickets\TicketReply.cshtml"
  
    Layout = "_EmailLayoutHtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"content\" style=\"padding: 0 24px 24px;\">\r\n        <div class=\"welcome\"\r\n             style=\"font-size: 20px; font-weight: 700; margin-bottom: 24px; font-family: \'Open Sans\', \'Lato\', sans-serif;\">\r\n            ");
#nullable restore
#line 10 "D:\Krenova\Project\PreSchool\Src\PreSchool.EmailTemplates\Views\Tickets\TicketReply.cshtml"
       Write(Model.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"greeting\" style=\"margin-bottom: 14px;\">\r\n            <span style=\"margin-right: 2px;\">Dear </span>\r\n            <span style=\"font-weight: 700;\">");
#nullable restore
#line 14 "D:\Krenova\Project\PreSchool\Src\PreSchool.EmailTemplates\Views\Tickets\TicketReply.cshtml"
                                       Write(Model.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>,\r\n        </div>\r\n\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Your ticket with following details has been updated with following details\r\n        </div>\r\n\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Ticket ID:");
#nullable restore
#line 22 "D:\Krenova\Project\PreSchool\Src\PreSchool.EmailTemplates\Views\Tickets\TicketReply.cshtml"
                 Write(Model.TicketId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Ticket Status:");
#nullable restore
#line 25 "D:\Krenova\Project\PreSchool\Src\PreSchool.EmailTemplates\Views\Tickets\TicketReply.cshtml"
                     Write(Model.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Ticket Date:");
#nullable restore
#line 28 "D:\Krenova\Project\PreSchool\Src\PreSchool.EmailTemplates\Views\Tickets\TicketReply.cshtml"
                   Write(Model.TicketDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Ticket Comment:");
#nullable restore
#line 31 "D:\Krenova\Project\PreSchool\Src\PreSchool.EmailTemplates\Views\Tickets\TicketReply.cshtml"
                      Write(Model.Comment);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Please feel free to reach to us with any additional questions or concerns.\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PreSchool.EmailTemplates.ViewModels.TicketReplyEmailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591