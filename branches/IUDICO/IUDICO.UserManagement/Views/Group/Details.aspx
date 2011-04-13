<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Group>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=UsManagRes.UserManagem.DetailsOf%> <%: Model.Name %></h2>

    <fieldset>
        <legend><%=UsManagRes.UserManagem.Users%></legend>

        <table>
        <tr>
            <th>
                <%=UsManagRes.UserManagem.Username%>
            </th>
            <th>
                <%=UsManagRes.UserManagem.Name%>
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
                    <%: Html.ActionLink(UsManagRes.UserManagem.RemoveUser, "RemoveUser", new { id = Model.Id, userRef = groupUser.User.Id })%>
                </td>
            </tr>
        <% } %>

        </table>

    </fieldset>
    <p>

        <%: Html.ActionLink(UsManagRes.UserManagem.Edit, "Edit", new { id = Model.Id })%> |
        <%: Html.ActionLink(UsManagRes.UserManagem.AddUser, "AddUsers", new { id = Model.Id })%> |
        <%: Html.ActionLink(UsManagRes.UserManagem.BackToList, "Index")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

