<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.DisciplineManagement.Models.ViewDataClasses.CreateTopicModel>" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>

<% Html.EnableClientValidation(); %>

<% using (Ajax.BeginForm("Create", "Topic", new { }, new AjaxOptions() { OnFailure = "onFailure", OnSuccess = "onCreateTopicSuccess" })) { %>
    <%: Html.ValidationSummary(true, IUDICO.DisciplineManagement.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

    <fieldset>
        <legend><%=IUDICO.DisciplineManagement.Localization.getMessage("Fields")%></legend>
            
        <%= Html.EditorForModel() %>

    </fieldset>

<% } %>

