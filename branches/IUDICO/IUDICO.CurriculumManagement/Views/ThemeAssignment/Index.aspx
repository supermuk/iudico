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
        Theme assignments for
        <%: (ViewData["Curriculum"] as Curriculum).Name%>
        curriculum and
        <%: ViewData["GroupName"]%>
        group
    </h2>
    <table>
        <tr>
            <th>
                Theme name
            </th>
            <th>
                Max score
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
                <%: Html.ActionLink("Edit", "Edit", new { ThemeAssignmentId = item.ThemeAssignment.Id }, null)%>
            </td>
        </tr>
        <% } %>
    </table>
    <div>
        <br />
        <%: Html.RouteLink("Back to curriculum assignments.", "CurriculumAssignments", new { action = "Index", CurriculumId = (ViewData["Curriculum"] as Curriculum).Id })%>
    </div>
</asp:Content>
