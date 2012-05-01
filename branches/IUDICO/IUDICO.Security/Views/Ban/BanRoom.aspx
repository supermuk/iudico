<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.BanRoomViewModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

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
            <td> <%= Html.ActionLink(Localization.GetMessage("Edit"), "EditRoom",new { room = item.Name }) %> |
             <%= Html.ActionLink(Localization.GetMessage("Delete"), "DeleteRoom", new { room = item.Name })%> | 
            <% if (item.Allowed)
                   { %>
                    <%: Html.ActionLink(Localization.GetMessage("Ban"), "RoomBan", new { room = item.Name })%> 
                <% }
                   else
                   { %>
                    <%:Html.ActionLink(Localization.GetMessage("Unban"), "RoomUnban", new { room = item.Name })%> 
                <% } %> 
            </td>
        <//tr>
        <%} %>
    </table>
    <%= Html.ActionLink(Localization.GetMessage("BackToSecurity"), "Index", "Security") %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
