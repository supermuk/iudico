<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.DisciplineManagement.Models.ViewDataClasses.CreateTopicModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

<% Html.EnableClientValidation(); %>

<form action="/TopicAction/Edit" data-onSuccess="onEditTopicSuccess" data-onFailure="onFailure">
    <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

   <%= Html.EditorForModel() %>
</form>
