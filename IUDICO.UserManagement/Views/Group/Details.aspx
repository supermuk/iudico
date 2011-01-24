<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Group>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details of <%: Model.Name %></h2>

    <fieldset>
        <legend>Users</legend>

        <table>
        <tr>
            <th>
                Username
            </th>
            <th>
                Name
            </th>
            <th></th>
        </tr>

        <% foreach(var groupUser in Model.GroupUsers) { %>
            <tr>
                <td>
                    <%: groupUser.User.Username %>
                </td>
                <td>
                    <%: groupUser.User.Name %>
                </td>
                <td>
                    <%: Html.ActionLink("Remove User", "RemoveUser", new { id = Model.Id, userRef = groupUser.User.Id })%>
                </td>
            </tr>
            <%: groupUser.User.Username %>
        <% } %>

        </table>

    </fieldset>
    <p>

        <%: Html.ActionLink("Edit", "Edit", new { id=Model.Id }) %> |
        <%: Html.ActionLink("Add Users", "AddUsers", new { id=Model.Id }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

