<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumChapterTopicModel>" %>

<%@  Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<span class="headerName"><%=Localization.GetMessage("Chapter")%>:</span>
<span class="headerValue"><%: ViewData["ChapterName"] %></span>
<span class="headerName"><%=Localization.GetMessage("Discipline")%>:</span>
<span class="headerName"><%=Localization.GetMessage("Group")%>:</span>
<span class="headerValue"><%: ViewData["GroupName"] %></span>

<% Html.EnableClientValidation(); %>
<form action="/CurriculumChapterTopic/Edit" data-onSuccess="OnCurriculumChapterTopicEdit" data-onFailure="onFailure">
	     <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        <% Html.RenderPartial("EditorForCurriculumChapterTopicModel", Model); %>
</form>