<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.BanComputerViewModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ban Computer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("Edit computers")%></h2>
    
    <%= Html.ActionLink("Add computer", "AddComputers", "Ban") %>
    <p></p>

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
            <td> <%: item.Computer.IpAddress%> </td>
            <td> <% if (item.RoomName != null)
                   { %>
                    <%: Html.Label(item.RoomName)%> 
                <% }
                   else
                   { %>
                    <%: Html.Label("None")%> 
                <% } %>  </td>
            <td> <%: item.Computer.CurrentUser%> </td>
            <td> <%: item.Computer.Banned %> </td>
            <td> <%= Html.ActionLink(Localization.GetMessage("Edit"), "EditComputer", new { computerIp = item.Computer.IpAddress })%> | 
                 <%= Html.ActionLink(Localization.GetMessage("Delete"), "DeleteComputer", new { computer = item.Computer.IpAddress })%>| 
                 <% if (item.Computer.Banned)
                   { %>
                    <%: Html.ActionLink(Localization.GetMessage("Unban"), "ComputerUnban", new { computer = item.Computer.IpAddress })%> 
                <% }
                   else
                   { %>
                    <%:Html.ActionLink(Localization.GetMessage("Ban"), "ComputerBan", new { computer = item.Computer.IpAddress })%> 
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
