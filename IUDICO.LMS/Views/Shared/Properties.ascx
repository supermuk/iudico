<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.Common.Models.Node>" %>

    <% Html.EnableClientValidation(); %>
    <% using (Ajax.BeginForm("SaveProperties", "Node", new { NodeId = Model.Id, CourseId = Model.CourseId }, new AjaxOptions() { OnFailure = "onSavePropertiesFailure", OnSuccess = "onSavePropertiesSuccess" }))
       {%>
        <%: Html.ValidationSummary(true) %>
        
        <%: Html.EditorForModel(Model) %>
        <p>
            <input type="submit" value="Save" />
        </p>
    <% } %>
