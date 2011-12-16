<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.BanRoomViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ban Room
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ban Room</h2>

    <table>
        <tr>
            <th> Name </th>
            <th> Allowed </th>
            <th> </th>            
        </tr>
        <% foreach (var item in Model.Rooms)
           { %>
        <tr>
            <td> <%: item.Name %> </td>
            <td> <%: item.Allowed %> </td>
            <td> <%= Html.ActionLink("Edit", "EditRoom",new { room = item.Name }) %> |
             <%= Html.ActionLink("Delete", "DeleteRoom", new { room = item.Name })%> | 
            <% if (item.Allowed)
                   { %>
                    <%: Html.ActionLink("Ban", "RoomBan", new { room = item.Name })%> 
                <% }
                   else
                   { %>
                    <%:Html.ActionLink("Unban", "RoomUnban", new { room = item.Name })%> 
                <% } %> 
            </td>
        <//tr>
        <%} %>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
