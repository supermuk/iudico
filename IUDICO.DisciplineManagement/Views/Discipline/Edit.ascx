<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.Common.Models.Shared.Discipline>" %>
<%@ Import Namespace="IUDICO.Common" %>
<% Html.EnableClientValidation(); %>
<form action="/DisciplineAction/Edit" data-onsuccess="onEditDisciplineSuccess" data-onfailure="onFailure">
<%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
<%= Html.EditorForModel() %>
</form>
