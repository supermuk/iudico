<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Timeline>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Timeline
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Group : <%: ViewData["GroupName"] %>
    </h2>
    <p>
        <%: Html.ActionLink("Add Timeline", "CreateTimeline") %>
        <%--<a id="DeleteMany" href="#">Delete Selected</a>--%>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                Start Date
            </th>
            <th>
                End Date
            </th>
            <th>
                Operation
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
            <tr id="item<%: item.Id %>">
                <td>
                    <input type="checkbox" id="<%= item.Id %>" />
                </td>
                <td>
                    <%: item.StartDate %>
                </td>
                <td>
                    <%: item.EndDate %>
                </td>
                <td>
                    <%: item.OperationRef %>
                </td>
                <td>
                    <%--<a href="javascript:deleteItem(<%: item.Id %>)">Delete</a>--%>--%>
                </td>
            </tr>
        <% } %>
    </table>
</asp:Content>