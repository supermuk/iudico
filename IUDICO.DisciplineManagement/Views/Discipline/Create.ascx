<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.Common.Models.Shared.Discipline>" %>
<%@ Import Namespace="IUDICO.Common" %>

<% Html.EnableClientValidation(); %>

<form action="/DisciplineAction/Create" data-onSuccess="onCreateDisciplineSuccess" data-onFailure="onFailure">
    <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
    <%= Html.EditorForModel() %>
</form>