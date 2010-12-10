<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Timeline>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Timeline
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Timeline</h2>
    <p>
        <%: Html.ActionLink("Add Group", "Add") %>
        <%--<a id="DeleteMany" href="#">Delete Selected</a>--%>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                Id
            </th>
            <th>
                Start Date
            </th>
            <th>
                End Date
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
                    |
                    <%: Html.ActionLink("Edit Timeline for Stages", "EditTimelineForStages", new { GroupID = item.ID }, null)%>
<%--                    |
                    <a href="javascript:deleteItem(<%: item.Id %>)">Delete</a>--%>
                </td>
            </tr>
        <% } %>
    </table>
</asp:Content>
