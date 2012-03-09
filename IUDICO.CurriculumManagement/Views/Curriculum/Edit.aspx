<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumModel>" %>

<%@  Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CurriculumManagement.Localization.getMessage("EditCurriculumFor")%> <%: ViewData["DisciplineName"]%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.CurriculumManagement.Localization.getMessage("EditCurriculumFor")%></h2>
    <h4><%: ViewData["DisciplineName"]%></h4>
    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true, IUDICO.CurriculumManagement.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

        <fieldset>
            <legend><%=IUDICO.CurriculumManagement.Localization.getMessage("Fields")%></legend>
            <%= Html.EditorForModel() %>
        </fieldset>
        <p>
            <input type="submit" value="<%=IUDICO.CurriculumManagement.Localization.getMessage("Update") %>" />
        </p>
    <% } %>

    <div>
        <br />
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackToList"), "Curriculums", new { action = "Index", DisciplineId = HttpContext.Current.Session["DisciplineId"] })%>
    </div>
</asp:Content>

