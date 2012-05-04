<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.Common.Models.Shared.Chapter>" %>
<%@ Import Namespace="IUDICO.Common" %>

<% Html.EnableClientValidation(); %>

<form action="/ChapterAction/Edit" data-onSuccess="onEditChapterSuccess" data-onFailure="onFailure">
    <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

      <fieldset>
        <legend><%=Localization.GetMessage("Fields")%></legend>
            
        <%= Html.EditorForModel() %>

    </fieldset>
</form>