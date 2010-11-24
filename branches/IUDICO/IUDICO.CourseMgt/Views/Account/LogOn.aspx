<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	LogOn
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>LogOn</h2>

    <form action="<%: Url.Action("LogOn", "Account")  %>" method="post">
	    <label for="loginIdentifier">OpenID: </label>
	    <input id="loginIdentifier" name="loginIdentifier" size="40" />
	    
        <input type="submit" value="Login" />
	</form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
