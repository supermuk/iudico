<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th>
                <%=UsManagRes.UserManagem.Name%>
            </th>
            <th>
                <%=UsManagRes.UserManagem.Username%>
            </th>
            <th>
                <%=UsManagRes.UserManagem.Active%>
            </th>
            <th>
                <%=UsManagRes.UserManagem.ApprovedBy%>
            </th>
            <th>
                <%=UsManagRes.UserManagem.CreationDate%>
            </th>
            <th>
                <%=UsManagRes.UserManagem.Groups%>
            </th>
            <th>
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.Username %>
            </td>
            <td>
                <%: item.IsApproved.ToString() %>
            </td>
            <td>
                <%: item.User1 != null ? item.User1.Username : string.Empty %>
            </td>
            <td>
                <%: item.CreationDate.ToString() %>
            </td>
            <td>
                <%: item.GroupsLine %>
            </td>
            <td>
                <% if (item.IsApproved)
                   { %>
                    <%: Html.ActionLink(UsManagRes.UserManagem.Deactivate, "Deactivate", new { id = item.Id })%> |
                <% }
                   else
                   { %>
                    <%: Html.ActionLink(UsManagRes.UserManagem.Activate, "Activate", new { id = item.Id })%> |
                <% } %>
                <%: Html.ActionLink(UsManagRes.UserManagem.Edit, "Edit", new { id = item.Id })%> |
                <%: Html.ActionLink(UsManagRes.UserManagem.Details, "Details", new { id = item.Id })%> |
                <%: Html.ActionLink(UsManagRes.UserManagem.AddToGroup, "AddToGroup", new { id = item.Id })%> |
                <%: Ajax.ActionLink(UsManagRes.UserManagem.Delete, "Delete", new { id = item.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + item.Username + "\"?", HttpMethod = "Delete", OnSuccess = "removeRow" })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink(UsManagRes.UserManagem.CreateNew, "Create")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%= Html.ResolveUrl("/Scripts/Microsoft/MicrosoftAjax.js")%>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("/Scripts/Microsoft/MicrosoftMvcAjax.js")%>" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function removeRow(data) {
            window.location = window.location;
        }
    </script>
</asp:Content>

