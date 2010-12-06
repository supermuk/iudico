<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    if (HttpContext.Current.User.Identity.IsAuthenticated)
    {
%>
    Logged in as <b><%: HttpContext.Current.User.Identity.Name%></b>. <%: Html.ActionLink("Logout", "Logout", "Account") %>
<%
    }
    else
    {
%>
    <form action="<%: Url.Action("Login", "Account")  %>" method="post">
	    <label for="loginIdentifier">OpenID: </label>
	    <input type="text" id="loginIdentifier" name="loginIdentifier" size="40" />
	    
        <input type="submit" value="Login" />
	</form>

    <form action="<%: Url.Action("LoginDefault", "Account") %>" method="post">
        <label for="loginUsername">Username: </label>
        <input type="text" id="loginUsername" name="loginUsername" size="40" />
        <input type="password" id="loginPassword" name="loginPassword" size="40" />

        <input type="submit" value="Login" />
    </form>
<%
    }
%>