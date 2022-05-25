#pragma checksum "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderDeliveryAttemptFailedSecond.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e3a2b7ee7719d18ebc79577475e882177f856be2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Orders_OrderDeliveryAttemptFailedSecond), @"mvc.1.0.view", @"/Views/Orders/OrderDeliveryAttemptFailedSecond.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e3a2b7ee7719d18ebc79577475e882177f856be2", @"/Views/Orders/OrderDeliveryAttemptFailedSecond.cshtml")]
    public class Views_Orders_OrderDeliveryAttemptFailedSecond : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<eCommerce.EmailTemplates.ViewModels.OrderDeliveryAttemptFailedSecondEmailViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderDeliveryAttemptFailedSecond.cshtml"
  
    Layout = "_EmailLayoutHtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"content\" style=\"padding: 0 24px 24px;\">\r\n        <div class=\"welcome\"\r\n             style=\"font-size: 20px; font-weight: 700; margin-bottom: 24px; font-family: \'Open Sans\', \'Lato\', sans-serif;\">\r\n            ");
#nullable restore
#line 10 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderDeliveryAttemptFailedSecond.cshtml"
       Write(Model.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"greeting\" style=\"margin-bottom: 14px;\">\r\n            <span style=\"margin-right: 2px;\">Dear </span>\r\n            <span style=\"font-weight: 700;\">");
#nullable restore
#line 14 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderDeliveryAttemptFailedSecond.cshtml"
                                       Write(Model.Fullname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>,\r\n        </div>\r\n\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Our multiple delivery attempt to deliver your order with ");
#nullable restore
#line 18 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderDeliveryAttemptFailedSecond.cshtml"
                                                                Write(Model.OrderId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            number, has failed due to following reason.\r\n        </div>\r\n\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Ordered ID:");
#nullable restore
#line 23 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderDeliveryAttemptFailedSecond.cshtml"
                  Write(Model.OrderId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Delivery Attempt Date 1:");
#nullable restore
#line 26 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderDeliveryAttemptFailedSecond.cshtml"
                               Write(Model.AttemptDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Delivery Attempt Date 2:");
#nullable restore
#line 29 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderDeliveryAttemptFailedSecond.cshtml"
                               Write(Model.SecondAttemptDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Delivery Address :");
#nullable restore
#line 32 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderDeliveryAttemptFailedSecond.cshtml"
                         Write(Model.DeliveryAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Reason for Delivery Fail :");
#nullable restore
#line 35 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderDeliveryAttemptFailedSecond.cshtml"
                                 Write(Model.Reason);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        </div>
        <div style=""margin-bottom: 14px;"">
            We request you to collect your order from our dispatch center location at <link for dispatch center>. If order is not collected within 7 days of this email, we will cancel the order and will proceed the refund. Please contact us at ");
#nullable restore
#line 38 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderDeliveryAttemptFailedSecond.cshtml"
                                                                                                                                                                                                                                               Write(Model.SupportLink);

#line default
#line hidden
#nullable disable
            WriteLiteral(" for any further assistance.\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<eCommerce.EmailTemplates.ViewModels.OrderDeliveryAttemptFailedSecondEmailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591