#pragma checksum "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2842995b6f7c1d6d6e5ffdf23fa3447a4442f2f1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Orders_OrderShipped), @"mvc.1.0.view", @"/Views/Orders/OrderShipped.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2842995b6f7c1d6d6e5ffdf23fa3447a4442f2f1", @"/Views/Orders/OrderShipped.cshtml")]
    public class Views_Orders_OrderShipped : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<eCommerce.EmailTemplates.ViewModels.OrderShippedEmailViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml"
  
    Layout = "_EmailLayoutHtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"content\" style=\"padding: 0 24px 24px;\">\r\n        <div class=\"welcome\"\r\n             style=\"font-size: 20px; font-weight: 700; margin-bottom: 24px; font-family: \'Open Sans\', \'Lato\', sans-serif;\">\r\n            ");
#nullable restore
#line 10 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml"
       Write(Model.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"greeting\" style=\"margin-bottom: 14px;\">\r\n            <span style=\"margin-right: 2px;\">Dear </span>\r\n            <span style=\"font-weight: 700;\">");
#nullable restore
#line 14 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml"
                                       Write(Model.Fullname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>,\r\n        </div>\r\n\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Your order with ");
#nullable restore
#line 18 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml"
                       Write(Model.OrderId);

#line default
#line hidden
#nullable disable
            WriteLiteral(" number is ready to be delivered at following location\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Ordered Date:");
#nullable restore
#line 21 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml"
                    Write(Model.OrderDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Ordered ID:");
#nullable restore
#line 24 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml"
                  Write(Model.OrderId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Invoice Number :");
#nullable restore
#line 27 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml"
                       Write(Model.InvoiceNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Total Amount :");
#nullable restore
#line 30 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml"
                     Write(Model.TotalAmout);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Mode of Payment :");
#nullable restore
#line 33 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml"
                        Write(Model.ModeOfPayment);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Delivery Date :");
#nullable restore
#line 36 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml"
                      Write(Model.DeliveryDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Delivery Address :");
#nullable restore
#line 39 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml"
                         Write(Model.DeliveryAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Please contact us at ");
#nullable restore
#line 42 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderShipped.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<eCommerce.EmailTemplates.ViewModels.OrderShippedEmailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591