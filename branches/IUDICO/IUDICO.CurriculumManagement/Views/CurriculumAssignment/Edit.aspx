<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumAssignmentModel>" %>

<%@  Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Assignment
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.CurriculumManagement.Localization.getMessage("EditAssignment")%></h2>

    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true, "Please correct the following error(s) and try again:")%>

        <fieldset>
            <legend>Fields</legend>

            <div class="editor-label">
                <%: Html.Label(IUDICO.CurriculumManagement.Localization.getMessage("ChooseGroup")+":") %>
            </div>
            <div>
                <%: Html.DropDownListFor(x => x.GroupId,Model.Groups)%>
            </div>
            
            <p>
                <input type="submit" value=<%=IUDICO.CurriculumManagement.Localization.getMessage("Update") %> />
            </p>
        </fieldset>
    <% } %>

    <div>
        <br />
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackToList"), "CurriculumAssignments", new { action = "Index", CurriculumId = HttpContext.Current.Session["CurriculumId"] })%>
    </div>
</asp:Content>

