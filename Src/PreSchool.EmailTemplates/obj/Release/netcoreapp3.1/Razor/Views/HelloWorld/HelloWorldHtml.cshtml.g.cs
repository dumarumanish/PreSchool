#pragma checksum "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\HelloWorld\HelloWorldHtml.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2df3b793db35e9d3e5dc5786329357a2fa524be3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HelloWorld_HelloWorldHtml), @"mvc.1.0.view", @"/Views/HelloWorld/HelloWorldHtml.cshtml")]
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
#line 1 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\HelloWorld\HelloWorldHtml.cshtml"
using eCommerce.EmailTemplates.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2df3b793db35e9d3e5dc5786329357a2fa524be3", @"/Views/HelloWorld/HelloWorldHtml.cshtml")]
    public class Views_HelloWorld_HelloWorldHtml : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<HelloWorldViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\HelloWorld\HelloWorldHtml.cshtml"
  
    Layout = "_EmailLayoutHtml";
    ViewContext.ViewData["EmailTitle"] = "Hello World!";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<p>\r\n    It looks like you may have just sent your first custom email!\r\n</p>\r\n\r\n\r\n");
#nullable restore
#line 14 "D:\Krenova\Project\eCommerce\Src\eCommerce.EmailTemplates\Views\HelloWorld\HelloWorldHtml.cshtml"
Write(await Html.PartialAsync("_EmailButton", new EmailButtonViewModel("Let's Go!", Model.ButtonLink)));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<p>\r\n    Derek Arends\r\n</p>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HelloWorldViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
