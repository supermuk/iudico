<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumAssignmentModel>" %>

<%@  Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Assignment
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.CurriculumManagement.Localization.getMessage("EditAssignmentFor")%></h2>
    <h4><%: ViewData["CurriculumName"]%></h4>
    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true, IUDICO.CurriculumManagement.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

        <fieldset>
            <legend><%=IUDICO.CurriculumManagement.Localization.getMessage("Fields")%></legend>

            <div class="editor-label">
                <%: Html.Label(IUDICO.CurriculumManagement.Localization.getMessage("ChooseGroup")) %>
            </div>
            <div>
                <%: Html.DropDownListFor(x => x.GroupId,Model.Groups)%>
            </div>
        </fieldset>
        <p>
            <input type="submit" value="<%=IUDICO.CurriculumManagement.Localization.getMessage("Update") %>" />
        </p>
    <% } %>

    <div>
        <br />
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackToList"), "CurriculumAssignments", new { action = "Index", CurriculumId = HttpContext.Current.Session["CurriculumId"] })%>
    </div>
</asp:Content>

