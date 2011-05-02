<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewThemeAssignmentModel>>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Theme Assignments
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("ThemeAssignmentsFor")%>
    </h2>
    <h4>
        <%: (ViewData["Curriculum"] as Curriculum).Name%>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("PrevNext")%>
        <%: (ViewData["Group"] as IUDICO.Common.Models.Group).Name %>
    </h4>
    <table>
        <tr>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("ThemeName")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("MaxScore")%>
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr id="item<%: item.ThemeAssignment.Id %>">
            <td>
                <%: item.Theme.Name %>
            </td>
            <td>
                <%: item.ThemeAssignment.MaxScore %>
            </td>
            <td>
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("Edit"), "Edit", new { ThemeAssignmentId = item.ThemeAssignment.Id }, null)%>
            </td>
        </tr>
        <% } %>
    </table>
    <div>
        <br />
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackCurriculumAssignments"), "CurriculumAssignments", new { action = "Index", CurriculumId = (ViewData["Curriculum"] as Curriculum).Id })%>
    </div>
</asp:Content>
