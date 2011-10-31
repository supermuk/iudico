<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Ban / Unban</h2>
    <table>
        <tr>
            <th> Computer <//th>
            <th> <%= Html.ActionLink("Add", "AddComputers", "Ban") %> <//th>
            <th> <%= Html.ActionLink("Edit", "EditComputer", "Ban") %> <//th>
            <th> <%= Html.ActionLink("Ban", "BanComputer", "Ban") %> <//th>
        </tr>
        <tr>
            <th> Room <//th>
            <th> <%= Html.ActionLink("Add", "AddRoom", "Ban") %> <//th>
            <th> <%= Html.ActionLink("Edit", "EditRoom", "Ban") %> <//th>
            <th> <%= Html.ActionLink("Ban", "BanRoom", "Ban") %> <//th>
        </tr>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
