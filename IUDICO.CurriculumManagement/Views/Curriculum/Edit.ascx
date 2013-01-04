<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumModel>" %>

<%@  Assembly Name="IUDICO.CurriculumManagement" %>

<%@ Import Namespace="IUDICO.Common" %>

<h2><%=Localization.GetMessage("EditCurriculumFor")%></h2>
    <h4><%: ViewData["DisciplineName"]%></h4>
    <% Html.EnableClientValidation(); %>
	 
	 <form action="/Curriculum/Edit" data-onSuccess="OnCurriculumEdit" data-onFailure="onFailure">
		 <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        <% Html.RenderPartial("EditorForCurriculumModel", Model); %>
	 </form>