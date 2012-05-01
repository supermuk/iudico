<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.Common.Models.NodeProperty>" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="IUDICO.Common" %>

    <% Html.EnableClientValidation(); %>
    <% using (Ajax.BeginForm("SaveProperties", "Node", new { NodeId = Model.NodeId, type = Model.Type, CourseId = Model.CourseId }, new AjaxOptions() { OnFailure = "onSavePropertiesFailure", OnSuccess = "onSavePropertiesSuccess" }))
       {%>
        <%: Html.ValidationSummary(true) %>
        
        <%: Html.EditorForModel(Model) %>
        <p>
            <input type="submit" value=<%=Localization.GetMessage("Save") %> />
        </p>
    <% } %>
