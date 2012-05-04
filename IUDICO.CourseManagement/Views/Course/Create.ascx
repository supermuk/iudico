<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.Common.Models.Shared.Course>" %>
<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="IUDICO.Common" %>

<% Html.EnableClientValidation(); %>

<form action="/Course/Create" data-onSuccess="onCreateCourseSuccess" data-onFailure="onFailure">
        <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

        <%: Html.EditorForModel() %>
</form>
