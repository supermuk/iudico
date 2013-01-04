<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumChapterModel>" %>

<%@  Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

    <span class="headerName"><%: Localization.GetMessage("Discipline")%>:</span>
    <span class="headerValue"><%: ViewData["DisciplineName"] %></span>
    <span class="headerName"><%: Localization.GetMessage("Group")%>:</span>
    <span class="headerValue"><%: ViewData["GroupName"] %></span>
	 <% Html.EnableClientValidation(); %>
	 
	 <form action="/CurriculumChapter/Edit" data-onSuccess="OnCurriculumChapterEdit" data-onFailure="onFailure">
	     <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        <% Html.RenderPartial("EditorForCurriculumChapterModel", Model); %>
	 </form>