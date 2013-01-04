<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumModel>" %>
<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<% Html.EnableClientValidation(); %>

<form action="/Curriculum/Create" data-onSuccess="OnCurriculumCreate" data-onFailure="onFailure">
        <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        <% Html.RenderPartial("EditorForCurriculumModel", Model); %>
</form>

