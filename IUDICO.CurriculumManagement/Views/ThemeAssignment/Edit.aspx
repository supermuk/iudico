<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.ThemeAssignment>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Theme Assignment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("EditThemeAssignment")%>
    </h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true, "Please correct the following error(s) and try again:")%>
    <fieldset>
        <legend><%=IUDICO.CurriculumManagement.Localization.getMessage("Fields")%></legend>
        <%= Html.EditorForModel() %>
        
        <p>
            <input type="submit" value=<%=IUDICO.CurriculumManagement.Localization.getMessage("Update")%> />
        </p>
    </fieldset>
    <% } %>

    <div>
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackThemeAssignmentList"), "ThemeAssignments", new { action = "Index", CurriculumAssignmentId = HttpContext.Current.Session["CurriculumAssignmentId"] })%>
    </div>
</asp:Content>
