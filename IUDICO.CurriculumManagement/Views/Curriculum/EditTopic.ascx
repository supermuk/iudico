<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumChapterTopicModel>" %>
<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>

<% Html.EnableClientValidation(); %>

<% using (Ajax.BeginForm("EditTopic", "Curriculum", new { }, new AjaxOptions() { OnFailure = "onFailure", OnSuccess = "onEditTopicSuccess" })) { %>
    <%: Html.ValidationSummary(true, IUDICO.CurriculumManagement.Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

    <fieldset>
        <legend><%=IUDICO.CurriculumManagement.Localization.GetMessage("Fields")%></legend>
            
        <%= Html.EditorForModel() %>

    </fieldset>

<% } %>

