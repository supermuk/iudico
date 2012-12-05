<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.BanComputerViewModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ban Computer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("Edit computers")%></h2>

     <table>
        <tr>
            <th> <%=Localization.GetMessage("IP") %> </th>
            <th> <%=Localization.GetMessage("Room") %> </th>
            <th> <%=Localization.GetMessage("CurrentUser") %> </th>
            <th> <%=Localization.GetMessage("Banned") %> </th>
            <th> </th>
        </tr>
        <% foreach (var item in Model.Computers)
           { %>
        <tr>
            <td> <%: item.IpAddress%> </td>
            <td> <% if (item.RoomRef != null)
                   { %>
                    <%: Html.Label(item.Room.Name)%> 
                <% }
                   else
                   { %>
                    <%: Html.Label("None")%> 
                <% } %>  </td>
            <td> <%: item.CurrentUser%> </td>
            <td> <%: item.Banned %> </td>
            <td> <%= Html.ActionLink(Localization.GetMessage("Edit"), "EditComputer",new IUDICO.Security.ViewModels.Ban.EditComputersViewModel(new IUDICO.Security.Models.Storages.Database.DatabaseBanStorage().GetComputer(item.IpAddress)) )%> | 
                 <%= Html.ActionLink(Localization.GetMessage("Delete"), "DeleteComputer", new { computer = item.IpAddress })%>| 
                 <% if (item.Banned)
                   { %>
                    <%: Html.ActionLink(Localization.GetMessage("Unban"), "ComputerUnban", new { computer = item.IpAddress })%> 
                <% }
                   else
                   { %>
                    <%:Html.ActionLink(Localization.GetMessage("Ban"), "ComputerBan", new { computer = item.IpAddress })%> 
                <% } %> 
            </td>
        <//tr>
        <%} %>
    </table>
    <%= Html.ActionLink(Localization.GetMessage("BackToBan"), "Index", "Ban") %>




</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
