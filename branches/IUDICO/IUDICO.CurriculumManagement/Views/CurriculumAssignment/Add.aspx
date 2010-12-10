<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add Group
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Add Group</h2>
    <table>
        <tr>
            <th>
            </th>
            <th>
                List of Groups
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
            <tr id="item<%: item.ID %>">
                <td>
                    <input type="checkbox" id="<%= item.ID %>" />
                </td>
                <td>
                    <%: item.ID %>
                </td>
                <td>
                    <%: item.Name %>
                </td>
                <td>
                    <%: Html.ActionLink("Edit Timeline", "EditTimeline", new { GroupID = item.ID })%>
                </td>
            </tr>
        <% } %>
    </table>
</asp:Content>

