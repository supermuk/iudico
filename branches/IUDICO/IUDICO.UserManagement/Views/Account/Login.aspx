<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=UsManagAcRes.UserManagementAccount.Login %></h2>

    <div><%= Html.ValidationSummary(true) %></div>

    <form action="<%: Url.Action("Login", "Account")  %>" method="post">
	    <label for="loginIdentifier">OpenID: </label>
	    <input type="text" id="loginIdentifier" name="loginIdentifier" size="40" />
	    
        <input type="submit" value=<%=UsManagAcRes.UserManagementAccount.Login %> />
	</form>

    <form action="<%: Url.Action("LoginDefault", "Account") %>" method="post">
        <label for="loginUsername"><%=UsManagAcRes.UserManagementAccount.Username %>: </label>
        <input type="text" id="loginUsername" name="loginUsername" size="40" />
        <label for="loginPassword"><%=UsManagAcRes.UserManagementAccount.Password %>: </label>
        <input type="password" id="loginPassword" name="loginPassword" size="40" />

        <input type="submit" value=<%=UsManagAcRes.UserManagementAccount.Login %> />
    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
