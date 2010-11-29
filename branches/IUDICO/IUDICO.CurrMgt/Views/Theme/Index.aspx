<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.CurrMgt.Controllers.ThemeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Themes(Courses)</h2>

    <p>
        <%: Html.ActionLink("Add course", "Create", new { StageID = Model.StageId})%>
        <a id="DeleteMany" href="#">Delete Selected</a>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                Id
            </th>
            <th>
                Course name
            </th>
            <th>
                Created
            </th>
            <th>
                Updated
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model.Themes)
           { %>
        <tr>
            <td>
                <input type="checkbox" id="<%= item.Id %>" />
            </td>
            <td>
                <%: item.Id %>
            </td>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Created) %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Updated) %>
            </td>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { ThemeID = item.Id, StageId = item.StageRef })%>
                |
                <%: Html.ActionLink("Up", "ThemeUp", new { ThemeID = item.Id, StageId = item.StageRef})%>
                |
                <%: Html.ActionLink("Down", "ThemeDown", new { ThemeID = item.Id, StageId = item.StageRef })%>
                |
                <%: Ajax.ActionLink("Delete", "Delete", new { ThemeID = item.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + item.Name + "\"?", HttpMethod = "Delete" })%>
            </td>
        </tr>
        <% } %>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
