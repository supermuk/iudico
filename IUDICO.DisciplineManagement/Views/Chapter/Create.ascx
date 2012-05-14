<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.Common.Models.Shared.Chapter>" %>
<%@ Import Namespace="IUDICO.Common" %>

<% Html.EnableClientValidation(); %>

<form action="/ChapterAction/Create" data-onSuccess="onCreateChapterSuccess" data-onFailure="onFailure">
    <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
    <%= Html.EditorForModel() %>
</form>


