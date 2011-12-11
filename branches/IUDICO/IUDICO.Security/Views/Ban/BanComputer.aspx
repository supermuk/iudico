<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.BanComputerViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ban Computer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ban Computer</h2>

     <table>
        <tr>
            <th> IP Address </th>
            <th> Room </th>
            <th> Current user </th>
            <th> Banned </th>
            <th> </th>
        </tr>
        <% foreach (var item in Model.Computers)
           { %>
        <tr>
            <td> <%: item.IpAddress%> </td>
            <td> <%: item.Room%> </td>
            <td> <%: item.CurrentUser%> </td>
            <td> <%: item.Banned %> </td>
            <td> <%= Html.ActionLink("Edit", "EditComputer", new { computer = item.IpAddress })%> | 
                 <%= Html.ActionLink("Delete", "DeleteComputer", new { computer = item.IpAddress })%>| 
                 <% if (item.Banned)
                   { %>
                    <%: Html.ActionLink("Unban", "ComputerUnban", new { computer = item.IpAddress })%> 
                <% }
                   else
                   { %>
                    <%:Html.ActionLink("Ban", "ComputerBan", new { computer = item.IpAddress })%> 
                <% } %> 
            </td>
        <//tr>
        <%} %>
    </table>




</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
