#pragma checksum "C:\Users\alxan\Git\SerialNotes2.0\SerialNotes\SerialNotes\Views\Add\Partials\AddNotification.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "01e4e009ca886d3e68ff6fda1807db3916631cf6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Add_Partials_AddNotification), @"mvc.1.0.view", @"/Views/Add/Partials/AddNotification.cshtml")]
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
#line 1 "C:\Users\alxan\Git\SerialNotes2.0\SerialNotes\SerialNotes\Views\Add\Partials\AddNotification.cshtml"
using SerialNotes;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01e4e009ca886d3e68ff6fda1807db3916631cf6", @"/Views/Add/Partials/AddNotification.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9af4978b9c2bfca24ef48e96efe5f8573634464", @"/Views/_ViewImports.cshtml")]
    public class Views_Add_Partials_AddNotification : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<int>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<div id=\"modal-notify\" class=\"modal\">\r\n    <div class=\"modal-dialog\">\r\n        <div class=\"modal-content\">\r\n            <h2>Уведомление</h2>\r\n            <div class=\"notify-content\">\r\n");
#nullable restore
#line 10 "C:\Users\alxan\Git\SerialNotes2.0\SerialNotes\SerialNotes\Views\Add\Partials\AddNotification.cshtml"
             if(@Model != 0){

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>Заметка добавленка</p>\r\n");
#nullable restore
#line 12 "C:\Users\alxan\Git\SerialNotes2.0\SerialNotes\SerialNotes\Views\Add\Partials\AddNotification.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>Ошибка добавления</p>\r\n");
#nullable restore
#line 16 "C:\Users\alxan\Git\SerialNotes2.0\SerialNotes\SerialNotes\Views\Add\Partials\AddNotification.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n           <button data-close type=\"button\" class=\"btn__form\"s>ок</button>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<int> Html { get; private set; }
    }
}
#pragma warning restore 1591
