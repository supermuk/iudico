<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.Common.Models.Shared.Course>" %>
<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>

<% Html.EnableClientValidation(); %>

<% using (Ajax.BeginForm("Create", "Course", new { }, new AjaxOptions() { OnFailure = "onFailure", OnSuccess = "onCreateCourseSuccess" })) { %>
        <%: Html.ValidationSummary(true, IUDICO.CourseManagement.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

        <%: Html.EditorForModel() %>

    <% } %>

