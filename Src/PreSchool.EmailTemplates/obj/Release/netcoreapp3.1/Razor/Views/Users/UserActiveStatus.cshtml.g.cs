#pragma checksum "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserActiveStatus.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6a5aa787c88a0d357952d3ba59c761e009d6f4c0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_UserActiveStatus), @"mvc.1.0.view", @"/Views/Users/UserActiveStatus.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6a5aa787c88a0d357952d3ba59c761e009d6f4c0", @"/Views/Users/UserActiveStatus.cshtml")]
    public class Views_Users_UserActiveStatus : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<eCommerce.EmailTemplates.ViewModels.UserActiveStatusEmailViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserActiveStatus.cshtml"
  
    Layout = "_EmailLayoutHtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<p>\r\n    Dear <b> ");
#nullable restore
#line 8 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserActiveStatus.cshtml"
        Write(Model.Username);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </b>,\r\n</p>\r\n<p>\r\n    This is to inform you that our administrative team, has kept your <b> User status </b> on <b>Active</b>, due to <b> ");
#nullable restore
#line 11 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserActiveStatus.cshtml"
                                                                                                                   Write(Model.Remark);

#line default
#line hidden
#nullable disable
            WriteLiteral(". </b>\r\n\r\n</p>\r\n<p>\r\n    We would request you to contact support department at <a");
            BeginWriteAttribute("href", " href=\"", 386, "\"", 419, 2);
            WriteAttributeValue("", 393, "mailto:", 393, 7, true);
#nullable restore
#line 15 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserActiveStatus.cshtml"
WriteAttributeValue(" ", 400, Model.SupportLink, 401, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("> ");
#nullable restore
#line 15 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserActiveStatus.cshtml"
                                                                                           Write(Model.SupportLink);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a> for further for further assistance.\r\n\r\n</p>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<eCommerce.EmailTemplates.ViewModels.UserActiveStatusEmailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
