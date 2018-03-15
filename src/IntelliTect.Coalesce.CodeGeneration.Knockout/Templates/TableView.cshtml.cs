using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using IntelliTect.Coalesce.Utilities;
// <auto-generated/>
namespace Razor
{
#line hidden
    using IntelliTect.Coalesce.DataAnnotations;
    using IntelliTect.Coalesce.Helpers;
    using IntelliTect.Coalesce.Knockout.Helpers;
    using IntelliTect.Coalesce.CodeGeneration.Templating.Razor;
    using IntelliTect.Coalesce.CodeGeneration.Knockout.Generators;
    using IntelliTect.Coalesce.TypeDefinition;
    using IntelliTect.Coalesce.Knockout.TypeDefinition;
    using IntelliTect.Coalesce.Utilities;
    public class TableViewTemplate : CoalesceTemplate<TableView>
    {
#pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            var b = new CodeBuilder();
            ClassViewModel model = Model.Model;
            string listViewModelsNamespace = "ListViewModels";
            if (!string.IsNullOrWhiteSpace(Model.AreaName))
            {
                listViewModelsNamespace = Model.AreaName + "." + listViewModelsNamespace;
            }
            if (!string.IsNullOrWhiteSpace(Model.ModulePrefix))
            {
                listViewModelsNamespace = Model.ModulePrefix + "." + listViewModelsNamespace;
            }
            b.Line("@using IntelliTect.Coalesce.Knockout.Helpers");
            b.Line("@{ ViewBag.Fluid = true; }");
            b.Line("<style>");
            b.Line("    td div a {");
            b.Line("        display: block;");
            b.Line("    }");
            b.Line("    th.sortable-header {");
            b.Line("        cursor: pointer;");
            b.Line("    }");
            b.Line("    th.sortable-header:hover {");
            b.Line("        background-color: #e6e6e6");
            b.Line("    }");
            b.Line("    .table-view-header{");
            b.Line("        padding: 10px 15px;");
            b.Line("    }");
            b.Line("</style>");
            Write($"<div");
            BeginWriteAttribute("class", " class=\"", 1191, "\"", 1241, 3);
            WriteAttributeValue("", 1199, "table-view", 1199, 10, true);
            WriteAttributeValue(" ", 1209, "obj-", 1210, 5, true);
            WriteAttributeValue("", 1214, model.Name.ToCamelCase(), 1214, 27, false);
            EndWriteAttribute();
            b.Line(">");
            b.Line("    <div class=\"table-view-header\">");
            b.Line("        <div class=\"clearfix\">");
            b.Line("            <h1 style=\"display: inline-block\">");
            b.Line($"                {model.Name.ToProperCase()} List");
            b.Line("                    <span style=\"font-size: .5em; padding-left: 20px;\"><a href=\"~/{model.ControllerName}/Docs\">API Docs</a></span>");
            b.Line("            </h1>");
            b.Line("            <span class=\"label label-info\" data-bind=\"fadeVisible: isLoading()\">Loading...</span>");
            b.Line("        </div>");
            b.Line("        <div class=\"clearfix\">");
            b.Line("            <div style=\"display: inline-block; font-size: 1.1em; margin-right: 10px;\">");
            b.Line("                <i class=\"fa fa-arrow-circle-left\" data-bind=\"enabled: previousPageEnabled() && !isLoading(), click: previousPage\"></i>");
            b.Line("                Page");
            b.Line("                <input data-bind=\"value: page\" style=\"width: 35px\">");
            b.Line("                of");
            b.Line("                <span data-bind=\"text: pageCount\"></span>");
            b.Line("                <i class=\"fa fa-arrow-circle-right\" data-bind=\"enabled: nextPageEnabled() && !isLoading(), click: nextPage\"></i>");
            b.Line("            </div>");
            b.Line("            <select data-bind=\"value:pageSize\" class=\"form-control\" style=\"width: 100px; display: inline-block\">");
            b.Line("                <option value=\"1\">1</option>");
            b.Line("                <option value=\"5\">5</option>");
            b.Line("                <option value=\"10\">10</option>");
            b.Line("                <option value=\"50\">50</option>");
            b.Line("                <option value=\"100\">100</option>");
            b.Line("                <option value=\"500\">500</option>");
            b.Line("                <option value=\"1000\">1000</option>");
            b.Line("            </select>");
            b.Line("            <input class=\"form-control pull-right\" style=\"width: 250px; margin-left: 20px\" data-bind=\"textInput: search\" placeholder=\"Search\" />");
            b.Line("            <div class=\"btn-group pull-right\">");
            if (model.SecurityInfo.IsCreateAllowed())
            {
                b.Line($"                        <a href=\"~/{model.ControllerName}/CreateEdit?@(ViewBag.Query)\" role=\"button\" class=\"btn btn-sm btn-default \"><i class=\"fa fa-plus\"></i> Create</a>");
            }
            b.Line("                    <button data-bind=\"click:load\" class=\"btn btn-sm btn-default \"><i class=\"fa fa-refresh\"></i> Refresh</button>");
            b.Line("                    @if (ViewBag.Editable)");
            b.Line("                    {");
            b.Line($"                    <a href=\"~/{model.ControllerName}/Table?@(ViewBag.Query)\" role=\"button\" class=\"btn btn-sm btn-default \"><i class=\"fa fa-lock\"></i> Make Read-only</a>");
            b.Line("                    }");
            if (model.SecurityInfo.IsEditAllowed())
            {
                b.Line("                        else");
                b.Line("                        {");
                b.Line($"                        <a href=\"~/{model.ControllerName}/TableEdit?@ViewBag.Query\" role=\"button\" class=\"btn btn-sm btn-default \"><i class=\"fa fa-pencil\"></i> Make Editable</a>");
                b.Line("                        }");
            }
            b.Line("                    <a href=\"#\" role=\"button\" class=\"btn btn-sm btn-default \" data-bind=\"attr:{href: downloadAllCsvUrl}\"><i class=\"fa fa-download\"></i> CSV</a>");
            if (model.SecurityInfo.IsCreateAllowed() && model.SecurityInfo.IsEditAllowed())
            {
                b.Line("                        <button role=\"button\" class=\"btn btn-sm btn-default \" data-bind=\"click: csvUploadUi\"><i class=\"fa fa-upload\"></i> CSV</button>");
            }
            b.Line("            </div>");
            b.Line("        </div>");
            b.Line("    </div>");
            b.Line("    <hr />");
            b.Line("    <div class=\"card table-view-body\">");
            b.Line("        <div class=\"card-body\">");
            b.Line("                <table class=\"table @(ViewBag.Editable ? \" editable\" : \"\" )\">");
            b.Line("            <thead>");
            b.Line("                <tr>");
            foreach (var prop in model.ClientProperties.Where(f => !f.IsHidden(HiddenAttribute.Areas.List)).OrderBy(f => f.EditorOrder))
            {
                if (!prop.Type.IsCollection)
                {
                    b.Line($"                            <th class=\"sortable-header\" data-bind=\"click: function(){{orderByToggle(\'{prop.Name}\')}}\">");
                    b.Line($"                                {prop.DisplayName}");
                    b.Line($"                                <i class=\"pull-right fa\" data-bind=\"css:{{\'fa-caret-up\': orderBy() == \'{prop.Name}\', \'fa-caret-down\': orderByDescending() == \'{prop.Name}\'}}\"></i>");
                    b.Line("                            </th>");
                }
                else
                {
                    b.Line($"                            <th>{prop.DisplayName}</th>");
                }
            }
            b.Line("                    <th style=\"width: 1%\">");
            b.Line("                    </th>");
            b.Line("                </tr>");
            b.Line("            </thead>");
            b.Line("            <tbody>");
            b.Line("                <!-- ko foreach: items -->");
            b.Line($"                <tr data-bind=\"css: {{'btn-warning': errorMessage()}}, attr: {{id: {model.PrimaryKey.Name.ToCamelCase()}}}\">");
            b.Line("                        @if (@ViewBag.Editable)");
            b.Line("                        {");
            foreach (var prop in model.ClientProperties.Where(f => !f.IsHidden(HiddenAttribute.Areas.List)).OrderBy(f => f.EditorOrder))
            {
                b.Line("                            ");
                Write($"                            <td");
                BeginWriteAttribute("class", " class=\"", 6312, "\"", 6341, 2);
                WriteAttributeValue("", 6320, "prop-", 6320, 5, true);
                WriteAttributeValue("", 6325, prop.JsonName, 6325, 16, false);
                EndWriteAttribute();
                b.Line($">{Display.PropertyHelper(prop, !prop.IsReadOnly, null, true)}</td>");
            }
            b.Line("                        }");
            b.Line("                        else");
            b.Line("                        {");
            foreach (var prop in model.ClientProperties.Where(f => !f.IsHidden(HiddenAttribute.Areas.List)).OrderBy(f => f.EditorOrder))
            {
                b.Line("                            ");
                Write($"                            <td");
                BeginWriteAttribute("class", " class=\"", 6767, "\"", 6796, 2);
                WriteAttributeValue("", 6775, "prop-", 6775, 5, true);
                WriteAttributeValue("", 6780, prop.JsonName, 6780, 16, false);
                EndWriteAttribute();
                b.Line($">{Display.PropertyHelper(prop, false, null, true)}</td>");
            }
            b.Line("                        }");
            b.Line("                    <td>");
            b.Line();
            b.Line("                        <!-- Editor buttons -->");
            b.Line("                        <div class=\"btn-group pull-right\" role=\"group\" style=\"display: inline-flex\">");
            if (model.ClientMethods.Any(f => !f.IsHidden(HiddenAttribute.Areas.List) && !f.IsStatic))
            {
                b.Line("                                <!-- Action buttons -->");
                b.Line("                                <div class=\"btn-group\" role=\"group\">");
                b.Line("                                    <button type=\"button\" class=\"btn btn-sm btn-default dropdown-toggle\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">");
                b.Line("                                        Actions <span class=\"caret\"></span>");
                b.Line("                                    </button>");
                b.Line("                                    <ul class=\"dropdown-menu\">");
                foreach (var method in model.ClientMethods.Where(f => !f.IsHidden(HiddenAttribute.Areas.List) && !f.IsStatic))
                {
                    b.Line($"                                            <li>{Display.MethodHelper(method)}</li>");
                }
                b.Line("                                    </ul>");
                b.Line("                                </div>");
            }
            Write($"                            ");
            if (model.SecurityInfo.IsEditAllowed())
            {
                b.Line("                                <a data-bind=\"attr:{ href: editUrl }\" class=\"btn btn-sm btn-default\">");
                b.Line("                                    <i class=\"fa fa-pencil\"></i>");
                b.Line("                                </a>");
            }
            Write($"                            ");
            if (model.SecurityInfo.IsDeleteAllowed())
            {
                b.Line("                                <button data-bind=\"click: deleteItemWithConfirmation\" class=\"btn btn-sm btn-danger\">");
                b.Line("                                    <i class=\"fa fa-remove\"></i>");
                b.Line("                                </button>");
            }
            b.Line();
            b.Line("                        </div>");
            b.Line("                        <div class=\"form-control-static\" data-bind=\"text: errorMessage\"></div>");
            b.Line("                    </td>");
            b.Line("                </tr>");
            b.Line("                <!-- /ko -->");
            b.Line("            </tbody>");
            b.Line("        </table>");
            b.Line("    </div>");
            b.Line("</div>");
            b.Line("</div>");
            if (model.ClientMethods.Any(f => f.IsStatic))
            {
                b.Line("    <div class=\"card\">");
                b.Line("        <div class=\"card-heading\">");
                b.Line("            <h3 class=\"card-title\">Actions</h3>");
                b.Line("        </div>");
                b.Line("        <div class=\"card-body\">");
                b.Line("            <table class=\"table\">");
                b.Line("                <thead>");
                b.Line("                    <tr>");
                b.Line("                        <th style=\"width: 20%;\">Action</th>");
                b.Line("                        <th style=\"width: 50%;\">Result</th>");
                b.Line("                        <th style=\"width: 20%;\">Successful</th>");
                b.Line("                        <th style=\"width: 10%;\"></th>");
                b.Line("                    </tr>");
                b.Line("                </thead>");
                b.Line("                <tbody>");
                foreach (MethodViewModel method in model.ClientMethods.Where(f => f.IsStatic))
                {
                    b.Line($"                        <tr data-bind=\"with: {method.JsVariable}\">");
                    b.Line("                            <td>");
                    if (method.ClientParameters.Any())
                    {
                        b.Line("                                    <button class=\"btn btn-default btn-xs\"");
                        b.Line($"                                            data-bind=\"click: function(){{$(\'#method-{method.Name}\').modal()}}\">");
                        b.Line($"                                        {method.DisplayName}");
                        b.Line("                                    </button>");
                    }
                    else
                    {
                        b.Line("                                    <button class=\"btn btn-default btn-xs\" data-bind=\"click: function(){ invoke() }\">");
                        b.Line($"                                        {method.DisplayName}");
                        b.Line("                                    </button>");
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
                    b.Line("                            </td>.");
                    b.Line("                            <td>");
                    b.Line("                                <span class=\"label label-info\" data-bind=\"fadeVisible: isLoading\">Loading</span>");
                    b.Line("                            </td>");
                    b.Line("                        </tr>");
                }
                b.Line("                </tbody>");
                b.Line("            </table>");
                b.Line("        </div>");
                b.Line("    </div>");
            }
            b.Line();
            b.Line();
            foreach (var method in model.ClientMethods.Where(f => f.IsStatic && f.ClientParameters.Any()))
            {
                Write($"{Knockout.ModalFor(method)}");
            }
            b.Line();
            b.Line("    @section Scripts");
            b.Line("    {");
            b.Line($"{ScriptHelper.StandardBinding(model)}    }}");
        }
#pragma warning restore 1998
    }
}