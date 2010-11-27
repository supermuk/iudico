<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Player Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Main page</h2>
    <p>Some text in main page</p>
    <p>
    <%: Html.ActionLink("Show Packages", "Index", "Package") %>
    </p>
</asp:Content>
