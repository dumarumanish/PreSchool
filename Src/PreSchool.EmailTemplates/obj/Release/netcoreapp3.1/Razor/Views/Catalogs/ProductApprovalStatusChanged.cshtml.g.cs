#pragma checksum "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Catalogs\ProductApprovalStatusChanged.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "629891413290f8a9e09f9e235e63240e8805c72a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Catalogs_ProductApprovalStatusChanged), @"mvc.1.0.view", @"/Views/Catalogs/ProductApprovalStatusChanged.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"629891413290f8a9e09f9e235e63240e8805c72a", @"/Views/Catalogs/ProductApprovalStatusChanged.cshtml")]
    public class Views_Catalogs_ProductApprovalStatusChanged : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<eCommerce.EmailTemplates.ViewModels.ProductApprovalStatusChangedEmailViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Catalogs\ProductApprovalStatusChanged.cshtml"
  
    Layout = "_EmailLayoutHtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"content\" style=\"padding: 0 24px 24px;\">\r\n        <div class=\"welcome\"\r\n             style=\"font-size: 20px; font-weight: 700; margin-bottom: 24px; font-family: \'Open Sans\', \'Lato\', sans-serif;\">\r\n            ");
#nullable restore
#line 10 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Catalogs\ProductApprovalStatusChanged.cshtml"
       Write(Model.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n\r\n\r\n        <div style=\"margin-bottom: 14px;\">\r\n \r\n           Product ");
#nullable restore
#line 16 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Catalogs\ProductApprovalStatusChanged.cshtml"
              Write(Model.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" has been ");
#nullable restore
#line 16 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Catalogs\ProductApprovalStatusChanged.cshtml"
                                          Write(Model.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral(".\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<eCommerce.EmailTemplates.ViewModels.ProductApprovalStatusChangedEmailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
