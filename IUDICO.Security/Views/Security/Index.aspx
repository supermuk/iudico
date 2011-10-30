<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Security
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>Ban / Unban</p>
    <ul title=" Ban / Unban">        
		<li> <%= Html.ActionLink("Add Computer", "AddComputers", "Ban") %></li>
        <li> <%= Html.ActionLink("Edit Computer", "EditComputer", "Ban") %></li>
        <li> <%= Html.ActionLink("Add Room", "AddRoom", "Ban") %></li>
        <li> <%= Html.ActionLink("Edit Room", "EditRoom", "Ban") %></li>        
	</ul>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
