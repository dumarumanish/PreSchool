#pragma checksum "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserStatusChange.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f6503d21800b368c016b162470fd0a68c796ce0b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_UserStatusChange), @"mvc.1.0.view", @"/Views/Users/UserStatusChange.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f6503d21800b368c016b162470fd0a68c796ce0b", @"/Views/Users/UserStatusChange.cshtml")]
    public class Views_Users_UserStatusChange : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<eCommerce.EmailTemplates.ViewModels.UserStatusChangeEmailViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserStatusChange.cshtml"
  
    Layout = "_EmailLayoutHtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<p>\r\n    Dear <b>");
#nullable restore
#line 8 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserStatusChange.cshtml"
       Write(Model.Username);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>,\r\n</p>\r\n<p>\r\n    This is to inform you that our administrative team, has kept your <b>user status </b> on <b> ");
#nullable restore
#line 11 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserStatusChange.cshtml"
                                                                                            Write(Model.UserStatus);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </b> status, due to <b> ");
#nullable restore
#line 11 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserStatusChange.cshtml"
                                                                                                                                      Write(Model.Remark);

#line default
#line hidden
#nullable disable
            WriteLiteral(".</b>\r\n\r\n</p>\r\n<p>\r\n    We would request you to contact support department at  <a");
            BeginWriteAttribute("href", " href=\"", 403, "\"", 436, 2);
            WriteAttributeValue("", 410, "mailto:", 410, 7, true);
#nullable restore
#line 15 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserStatusChange.cshtml"
WriteAttributeValue(" ", 417, Model.SupportLink, 418, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("> ");
#nullable restore
#line 15 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\Users\UserStatusChange.cshtml"
                                                                                            Write(Model.SupportLink);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>  for further updates on activation of your membership.\r\n\r\n</p>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<eCommerce.EmailTemplates.ViewModels.UserStatusChangeEmailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
