using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using IntelliTect.Coalesce.Utilities;
// <auto-generated/>
namespace Razor
{
#line hidden
    using IntelliTect.Coalesce.CodeGeneration.Templating.Razor;
    using IntelliTect.Coalesce.CodeGeneration.Knockout.Generators;
    using IntelliTect.Coalesce.TypeDefinition;
    using IntelliTect.Coalesce.Utilities;
    using IntelliTect.Coalesce.DataAnnotations;
    using IntelliTect.Coalesce.Knockout.Helpers;
    using IntelliTect.Coalesce.Knockout.TypeDefinition;
    public class CreateEditViewTemplate : CoalesceTemplate<CreateEditView>
    {
#pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            var b = new CodeBuilder();
            ClassViewModel model = Model.Model;
            string viewModelsNamespace = "ViewModels";
            if (!string.IsNullOrWhiteSpace(Model.AreaName))
            {
                viewModelsNamespace = Model.AreaName + "." + viewModelsNamespace;
            }
            if (!string.IsNullOrWhiteSpace(Model.ModulePrefix))
            {
                viewModelsNamespace = Model.ModulePrefix + "." + viewModelsNamespace;
            }
            b.Line("@using IntelliTect.Coalesce.Knockout.Helpers");
            b.Line("<div class=\"panel panel-default\">");
            b.Line("    <div class=\"panel-heading\">");
            b.Line("        <div class=\"btn-group pull-right\">");
            b.Line("            <button onclick=\"window.history.back()\" class=\"btn btn-sm btn-default\">Back</button>");
            b.Line("            <button data-bind=\"click:function() { load(); }\" class=\"btn btn-sm btn-default\">Refresh</button>");
            b.Line("        </div>");
            b.Line($"        <h1 class=\"clearfix\" style=\"display:inline-block;\">{model.Name.ToProperCase()}</h1>");
            b.Line("        <span class=\"label label-info\" data-bind=\"fadeVisible: isLoading()\">Loading...</span>");
            b.Line("    </div>");
            b.Line("    <div class=\"panel-body\">");
            b.Line("        <div class=\"form-horizontal\">");
            b.Line("            <div class=\"form-group btn-warning\" style=\"display: none;\" data-bind=\"if: errorMessage(), visible: errorMessage()\">");
            b.Line("                <label class=\"col-md-4 control-label\">Error</label>");
            b.Line("                <div class=\"col-md-8\">");
            b.Line("                    <div class=\"form-control-static\" data-bind=\"text: errorMessage\"></div>");
            b.Line("                </div>");
            b.Line("            </div>");
            foreach (var prop in model.ClientProperties.Where(f => !f.IsHidden(HiddenAttribute.Areas.Edit)).OrderBy(f => f.EditorOrder))
            {
                b.Line("                <div class=\"form-group\">");
                b.Line($"                    <label class=\"col-md-4 control-label\">{prop.DisplayName}</label>");
                if (prop.IsPOCO && prop.PureTypeOnContext)
                {
                    b.Line($"{Display.PropertyHelperWithSurroundingDiv(prop, !prop.IsReadOnly, Model.AreaName, 7)}                        <div class=\"col-md-1\" data-bind=\"with: {prop.JsVariableForBinding()}\">");
                    b.Line("                            <a data-bind=\"attr: {href: editUrl}\" class=\"btn btn-default pull-right\"><i class=\"fa fa-ellipsis-h \"></i></a>");
                    b.Line("                        </div>");
                }
                else
                {
                    Write($"{Display.PropertyHelperWithSurroundingDiv(prop, !prop.IsReadOnly, Model.AreaName)}");
                }
                b.Line("                </div>");
            }
            b.Line("        </div>");
            b.Line("    </div>");
            b.Line("    <div class=\"panel-body\">");
            if (model.ClientMethods.Any(f => !f.IsStatic))
            {
                b.Line("            <div class=\"panel panel-default\">");
                b.Line("                <div class=\"panel-heading\">");
                b.Line("                    <h4>Actions</h4>");
                b.Line("                </div>");
                b.Line("                <table class=\"table\">");
                b.Line("                    <tr>");
                b.Line("                        <th style=\"width:20%;\">Action</th>");
                b.Line("                        <th style=\"width:50%;\">Result</th>");
                b.Line("                        <th style=\"width:20%;\">Successful</th>");
                b.Line("                        <th style=\"width:10%;\"></th>");
                b.Line("                    </tr>");
                foreach (MethodViewModel method in model.ClientMethods.Where(f => !f.IsStatic))
                {
                    b.Line($"                        <tr data-bind=\"with: {method.JsVariable}\">");
                    b.Line("                            <td>");
                    if (method.ClientParameters.Any())
                    {
                        b.Line("                                <button class=\"btn btn-default btn-xs\" ");
                        b.Line($"                                        data-bind=\"click: function(){{$(\'#method-{method.Name}\').modal()}}\">");
                        b.Line($"                                    {method.DisplayName}");
                        b.Line("                                </button>");
                    }
                    else
                    {
                        b.Line("                                <button class=\"btn btn-default btn-xs\" data-bind=\"click: function(){ invoke() }\">");
                        b.Line($"                                    {method.DisplayName}");
                        b.Line("                                </button>");
                    }
                    b.Line("                            </td>");
                    b.Line("                            <td>");
                    if (method.ResultType.IsCollection)
                    {
                        b.Line("                                    <ul data-bind=\"foreach: result\">");
                        b.Line("                                        <li class=\"\" data-bind=\"text: $data\"></li>");
                        b.Line("                                    </ul>");
                    }
                    else if (method.ResultType.HasClassViewModel)
                    {
                        b.Line("                                    <dl class=\"dl-horizontal\" data-bind=\"with: result\">");
                        foreach (var prop in method.ResultType.ClassViewModel.ClientProperties.Where(f => !f.IsHidden(HiddenAttribute.Areas.Edit)))
                        {
                            b.Line($"                                            <dt>{prop.DisplayName}</dt>");
                            b.Line($"                                            <dd data-bind=\"text: {prop.JsVariableForBinding()}\"></dd>");
                        }
                        b.Line("                                    </dl>");
                    }
                    else
                    {
                        b.Line("                                    <span data-bind=\"text: result\"></span>");
                    }
                    b.Line("                            </td>");
                    b.Line("                            <td>");
                    b.Line("                                <span data-bind=\"text: wasSuccessful\"></span>");
                    b.Line("                                <span data-bind=\"text: message\"></span>");
                    b.Line("                            </td>");
                    b.Line("                            <td>");
                    b.Line("                                <span class=\"label label-info\" data-bind=\"fadeVisible: isLoading\">Loading</span>");
                    b.Line("                            </td>");
                    b.Line("                        </tr>");
                }
                b.Line("                </table>");
                b.Line("            </div>");
            }
            b.Line("    </div>");
            b.Line("</div>");
            b.Line();
            foreach (var method in model.ClientMethods.Where(f => !f.IsStatic && f.ClientParameters.Any()))
            {
                Write($"{Knockout.ModalFor(method)}");
            }
            b.Line();
            b.Line("    @section Scripts");
            b.Line("    {");
            b.Line("    <script>");
            b.Line($"        var model = new {viewModelsNamespace}.{model.ViewModelClassName}();");
            b.Line("        model.includes = \"Editor\";");
            b.Line("        model.saveCallbacks.push(function(obj){");
            b.Line("            // If there is a new id, set the one for this page");
            b.Line("            if (!Coalesce.Utilities.GetUrlParameter('id')){");
            b.Line("                if (model.myId) {");
            b.Line("                    var newUrl = Coalesce.Utilities.SetUrlParameter(window.location.href, \"id\", model.myId);");
            b.Line("                    window.history.replaceState(null, window.document.title, newUrl);");
            b.Line("                }");
            b.Line("            }");
            b.Line("        });");
            b.Line("        @if (ViewBag.Id != null)");
            b.Line("                {");
            b.Line("                    @:model.load(\'@ViewBag.Id\');");
            b.Line("                }");
            b.Line("        @foreach (var kvp in ViewBag.ParentIds)");
            b.Line("                {");
            b.Line("                    @:model.@(((string)(@kvp.Key)))(@kvp.Value);");
            b.Line("                }");
            b.Line();
            b.Line("        window.onbeforeunload = function(){");
            b.Line("            if (model.isDirty()) model.save();");
            b.Line("        }");
            b.Line("        model.coalesceConfig.autoSaveEnabled(false);");
            b.Line("        model.loadChildren(function() {");
            b.Line("            ko.applyBindings(model);");
            b.Line("            model.coalesceConfig.autoSaveEnabled(true);");
            b.Line("        });");
            b.Line("    </script>");
            b.Line("    }");
        }
#pragma warning restore 1998
    }
}