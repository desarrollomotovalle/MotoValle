#pragma checksum "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Models\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c91522efe015e27e3f291fec0753dcc8d70f1842"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Models_Views_Shared_Error), @"mvc.1.0.view", @"/Models/Views/Shared/Error.cshtml")]
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
#line 1 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Models\Views\_ViewImports.cshtml"
using motovalle.Ecommerce;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Models\Views\_ViewImports.cshtml"
using motovalle.Ecommerce.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Models\Views\_ViewImports.cshtml"
using motovalle.Ecommerce.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Models\Views\_ViewImports.cshtml"
using Ecommerce.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Models\Views\_ViewImports.cshtml"
using motovalle.Ecommerce.Models.ViewComponents;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c91522efe015e27e3f291fec0753dcc8d70f1842", @"/Models/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9d9751ebeea2a66464bceb63213afb7dc7c6723", @"/Models/Views/_ViewImports.cshtml")]
    public class Models_Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Models\Views\Shared\Error.cshtml"
  
    ViewData["Title"] = "Error";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<section class=""b-pageHeader error"">
    <div class=""container"">
        <h1 class=""wow zoomInLeft"" data-wow-delay=""0.7s"">Error</h1>
        <div class=""b-pageHeader__search wow zoomInRight"" data-wow-delay=""0.7s"">
            <h3>Lo sentímos</h3>
        </div>
    </div>
</section><!--b-pageHeader-->


<section class=""b-contacts s-shadow"">
    <div class=""container"">
        <div class=""row wow zoomInUp"" data-wow-delay=""0.5s"">
            <h1 class=""text-danger"">Error</h1>
            <h2 class=""text-danger"">Un error ha ocurrido mientras se procesaba tu petición</h2>

");
#nullable restore
#line 23 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Models\Views\Shared\Error.cshtml"
             if (Model.ShowRequestId)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>\r\n                    <strong>Request ID:</strong> <code>");
#nullable restore
#line 26 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Models\Views\Shared\Error.cshtml"
                                                  Write(Model.RequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</code>\r\n                </p>\r\n");
#nullable restore
#line 28 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Models\Views\Shared\Error.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            <h3>Development Mode</h3>
            <p>
                Swapping to <strong>Development</strong> environment will display more detailed information about the error that occurred.
            </p>
            <p>
                <strong>The Development environment shouldn't be enabled for deployed applications.</strong>
                It can result in displaying sensitive information from exceptions to end users.
                For local debugging, enable the <strong>Development</strong> environment by setting the <strong>ASPNETCORE_ENVIRONMENT</strong> environment variable to <strong>Development</strong>
                and restarting the app.
            </p>
        </div>
    </div>
</section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
