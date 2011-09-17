<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    if (HttpContext.Current.User.Identity.IsAuthenticated)
    {
%>
    <%=IUDICO.LMS.Localization.getMessage("LoggedInAs") %> <b><%: HttpContext.Current.User.Identity.Name%></b>. <%: Html.ActionLink(IUDICO.LMS.Localization.getMessage("Logout"), "Logout", "Account") %>
<%
    }
    else
    {
%>
    <form action="<%: Url.Action("Login", "Account")  %>" method="post">
	    <label for="loginIdentifier"><%=IUDICO.LMS.Localization.getMessage("OpenID") %> </label>
	    <input type="text" id="loginIdentifier" name="loginIdentifier" size="40" />
	    
        <input type="submit" value=<%=IUDICO.LMS.Localization.getMessage("Login") %> />
	</form>

    <form action="<%: Url.Action("LoginDefault", "Account") %>" method="post">
        <label for="loginUsername"><%=IUDICO.LMS.Localization.getMessage("Loginn") %></label>
        <input type="text" id="loginUsername" name="loginUsername" size="40" />
        <input type="password" id="loginPassword" name="loginPassword" size="40" />

        <input type="submit" value=<%=IUDICO.LMS.Localization.getMessage("Login") %> />
    </form>
<%
    }
%>