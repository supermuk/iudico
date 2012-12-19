<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.BanRoomViewModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ban Room
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("Ban room") %></h2>
    
    <%= Html.ActionLink("Add room", "AddRoom", "Ban") %>
    
    <p></p>

    <table>
        <tr>
            <th> <%=Localization.GetMessage("Name")%> </th>
            <th> <%=Localization.GetMessage("Allowed")%> </th>
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
        </tr>
        <%} %>
    </table>
    
    <p></p>

    <%= Html.ActionLink(Localization.GetMessage("BackToBan"), "Index", "Ban") %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
