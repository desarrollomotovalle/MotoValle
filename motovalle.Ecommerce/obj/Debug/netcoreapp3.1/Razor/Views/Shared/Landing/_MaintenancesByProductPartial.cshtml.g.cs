#pragma checksum "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "da12f45b706be446e103590027b310a0d174112c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Landing__MaintenancesByProductPartial), @"mvc.1.0.view", @"/Views/Shared/Landing/_MaintenancesByProductPartial.cshtml")]
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
#line 1 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\_ViewImports.cshtml"
using motovalle.Ecommerce;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\_ViewImports.cshtml"
using motovalle.Ecommerce.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\_ViewImports.cshtml"
using motovalle.Ecommerce.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\_ViewImports.cshtml"
using Ecommerce.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\_ViewImports.cshtml"
using motovalle.Ecommerce.Models.ViewComponents;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da12f45b706be446e103590027b310a0d174112c", @"/Views/Shared/Landing/_MaintenancesByProductPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9d9751ebeea2a66464bceb63213afb7dc7c6723", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Landing__MaintenancesByProductPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Ecommerce.Models.Entities.ProductMaintenances>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
  
    var index = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"wow zoomInUp\">\r\n");
#nullable restore
#line 7 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
     if (Model.Any())
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"panel-group\" id=\"maintenanceAccordion\">\r\n");
#nullable restore
#line 10 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
             foreach (var item in Model)
            {
                var collapseIndex = Guid.NewGuid().ToString();

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <div class=""panel panel-default"">
                    <div class=""panel-heading custom"">
                        <h4 class=""panel-title"">
                            <a class=""accordion-toggle"" data-toggle=""collapse"" data-parent=""#maintenanceAccordion""");
            BeginWriteAttribute("href", " href=\"", 607, "\"", 644, 1);
#nullable restore
#line 16 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
WriteAttributeValue("", 614, Html.Raw($"#{collapseIndex}"), 614, 30, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                ");
#nullable restore
#line 17 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
                           Write(item.MaintenanceIndex);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 17 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
                                                    Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 17 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
                                                                Write(Html.Raw(item.SalesPrice > 0m ? $"Precio: <b>{item.SalesPrice:c}</b>" : string.Empty));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </a>\r\n                        </h4>\r\n                    </div>\r\n                    <div");
            BeginWriteAttribute("id", " id=\"", 922, "\"", 956, 1);
#nullable restore
#line 21 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
WriteAttributeValue("", 927, Html.Raw($"{collapseIndex}"), 927, 29, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\"", 957, "\"", 1032, 3);
            WriteAttributeValue("", 965, "panel-collapse", 965, 14, true);
            WriteAttributeValue(" ", 979, "collapse", 980, 9, true);
#nullable restore
#line 21 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
WriteAttributeValue(" ", 988, Html.Raw(index == 0 ? "in" : string.Empty), 989, 43, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <div class=\"panel-body\">\r\n                            ");
#nullable restore
#line 23 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
                       Write(Html.Raw(item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 27 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
                index++;
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n");
#nullable restore
#line 30 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""alert alert-info"" role=""alert"">
            <h4 class=""alert-heading""><i class=""fa fa-info fa-3x""></i> No hay información para mostrar</h4>
            <p>Hemos consultado en nuestra base de datos y no hemos encontrado registros para mostrar.</p>
");
#nullable restore
#line 36 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
               await Html.RenderPartialAsync("_ContactUs"); 

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n");
#nullable restore
#line 38 "E:\Desarrollos\Motovalle\motovalle.Ecommerce\Views\Shared\Landing\_MaintenancesByProductPartial.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Ecommerce.Models.Entities.ProductMaintenances>> Html { get; private set; }
    }
}
#pragma warning restore 1591
