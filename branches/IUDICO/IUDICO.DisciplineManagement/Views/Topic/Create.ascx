<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.DisciplineManagement.Models.ViewDataClasses.CreateTopicModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

<% Html.EnableClientValidation(); %>

<form action="/TopicAction/Create" data-onSuccess="onCreateTopicSuccess" data-onFailure="onFailure">
    <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

    <fieldset>
        <legend><%=Localization.GetMessage("Fields")%></legend>
            
        <%= Html.EditorForModel() %>

    </fieldset>
</form>

