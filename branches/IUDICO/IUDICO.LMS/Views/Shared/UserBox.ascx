<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    if (HttpContext.Current.User.Identity.IsAuthenticated)
    {
%>
    <%=ViewRes.LMS.LoggedInAs %> <b><%: HttpContext.Current.User.Identity.Name%></b>. <%: Html.ActionLink(ViewRes.LMS.Logout, "Logout", "Account") %>
<%
    }
    else
    {
%>
    <form action="<%: Url.Action("Login", "Account")  %>" method="post">
	    <label for="loginIdentifier"><%=ViewRes.LMS.OpenID %> </label>
	    <input type="text" id="loginIdentifier" name="loginIdentifier" size="40" />
	    
        <input type="submit" value=<%=ViewRes.LMS.Login %> />
	</form>

    <form action="<%: Url.Action("LoginDefault", "Account") %>" method="post">
        <label for="loginUsername"><%=ViewRes.LMS.Username %></label>
        <input type="text" id="loginUsername" name="loginUsername" size="40" />
        <input type="password" id="loginPassword" name="loginPassword" size="40" />

        <input type="submit" value=<%=ViewRes.LMS.Login %> />
    </form>
<%
    }
%>