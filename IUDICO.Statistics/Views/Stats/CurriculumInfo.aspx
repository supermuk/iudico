<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Curriculum>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CurriculumInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink("<- back", "Index") %>
    <h2>Curriculum Info</h2>

    <table border="2" cellpadding="2" cellspacing="2">
    <tr>
    <th>Curriculum ID</th>
    <th>Curriculum name</th>
    <th>Themes</th>
    <th>Groups ID</th>
    <th>Max point</th>
    </tr>
    <tr>
    <td><%: Model.CurriculumId %></td>
    <td><%: Model.CurriculumName %></td>
    <td><% foreach (IUDICO.Statistics.Models.Theme theme in Model.Themes)
           {%>
    <%: theme.Name + " "%>
    <% } %>
    </td>
    <td><% foreach (int id in Model.GroupsId)  { %>
    <%: id + " " %>
    <% } %></td>
    <td> <%: Model.GetMaxPointsFromCurriculum()  %> </td>
    </tr>
    </table>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
