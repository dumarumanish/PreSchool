#pragma checksum "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "59e0a93fc080ac157a25e756adb3d686bf40a5df"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Orders_OrderReadyForCollection), @"mvc.1.0.view", @"/Views/Orders/OrderReadyForCollection.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59e0a93fc080ac157a25e756adb3d686bf40a5df", @"/Views/Orders/OrderReadyForCollection.cshtml")]
    public class Views_Orders_OrderReadyForCollection : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<eCommerce.EmailTemplates.ViewModels.OrderReadyForCollectionEmailViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml"
  
    Layout = "_EmailLayoutHtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"content\" style=\"padding: 0 24px 24px;\">\r\n        <div class=\"welcome\"\r\n             style=\"font-size: 20px; font-weight: 700; margin-bottom: 24px; font-family: \'Open Sans\', \'Lato\', sans-serif;\">\r\n            ");
#nullable restore
#line 10 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml"
       Write(Model.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"greeting\" style=\"margin-bottom: 14px;\">\r\n            <span style=\"margin-right: 2px;\">Dear </span>\r\n            <span style=\"font-weight: 700;\">");
#nullable restore
#line 14 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml"
                                       Write(Model.Fullname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>,\r\n        </div>\r\n\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Your order with ");
#nullable restore
#line 18 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml"
                       Write(Model.OrderId);

#line default
#line hidden
#nullable disable
            WriteLiteral(" number has been completed and is ready to be delivered.\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Ordered Date:");
#nullable restore
#line 21 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml"
                    Write(Model.OrderDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Ordered ID:");
#nullable restore
#line 24 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml"
                  Write(Model.OrderId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Invoice Number :");
#nullable restore
#line 27 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml"
                       Write(Model.InvoiceNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Total Amount :");
#nullable restore
#line 30 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml"
                     Write(Model.TotalAmout);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Mode of Payment :");
#nullable restore
#line 33 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml"
                        Write(Model.ModeOfPayment);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Delivery Date :");
#nullable restore
#line 36 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml"
                      Write(Model.DeliveryDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Delivery Address :");
#nullable restore
#line 39 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml"
                         Write(Model.DeliveryAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div style=\"margin-bottom: 14px;\">\r\n            Please contact us at ");
#nullable restore
#line 42 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Orders\OrderReadyForCollection.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<eCommerce.EmailTemplates.ViewModels.OrderReadyForCollectionEmailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
