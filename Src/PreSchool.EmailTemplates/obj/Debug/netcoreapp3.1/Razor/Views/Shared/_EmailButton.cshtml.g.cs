#pragma checksum "D:\Krenova\Project\PreSchool\Src\PreSchool.EmailTemplates\Views\Shared\_EmailButton.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b26c75c2fe2bc012f8e110cf623668f2f1f337a1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__EmailButton), @"mvc.1.0.view", @"/Views/Shared/_EmailButton.cshtml")]
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
#nullable restore
#line 1 "D:\Krenova\Project\PreSchool\Src\PreSchool.EmailTemplates\Views\Shared\_EmailButton.cshtml"
using PreSchool.EmailTemplates.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b26c75c2fe2bc012f8e110cf623668f2f1f337a1", @"/Views/Shared/_EmailButton.cshtml")]
    public class Views_Shared__EmailButton : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EmailButtonViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
    <tr>
        <td bgcolor=""#ffffff"" align=""center"" style=""padding: 30px;"">
            <table border=""0"" cellspacing=""0"" cellpadding=""0"">
                <tr>
                    <td align=""center"" style=""border-radius: 30px;"" bgcolor=""#0088f3"">
                        <a");
            BeginWriteAttribute("href", " href=\"", 420, "\"", 437, 1);
#nullable restore
#line 10 "D:\Krenova\Project\PreSchool\Src\PreSchool.EmailTemplates\Views\Shared\_EmailButton.cshtml"
WriteAttributeValue("", 427, Model.Url, 427, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" target=\"_blank\" style=\"font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; padding: 15px 25px; display: inline-block;\">\r\n                            ");
#nullable restore
#line 11 "D:\Krenova\Project\PreSchool\Src\PreSchool.EmailTemplates\Views\Shared\_EmailButton.cshtml"
                       Write(Model.Text);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </a>\r\n                    </td>\r\n                </tr>\r\n            </table>\r\n        </td>\r\n    </tr>\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EmailButtonViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
