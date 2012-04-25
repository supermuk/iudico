<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<IUDICO.Common.Models.Shared.ShareUser>>" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>

<% Html.EnableClientValidation(); %>

<% using (Ajax.BeginForm("Share", "Discipline", new { }, new AjaxOptions { OnFailure = "onFailure", OnSuccess = "onShareDisciplineSuccess" }))
    {%>
        <%: Html.ValidationSummary(true, IUDICO.DisciplineManagement.Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        
        <% Html.RenderPartial("ShareUserList", Model); %>
<% } %>