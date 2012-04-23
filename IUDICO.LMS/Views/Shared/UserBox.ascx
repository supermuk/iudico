<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    if (HttpContext.Current.User.Identity.IsAuthenticated)
    {
%>
    <div>
    <%=IUDICO.LMS.Localization.getMessage("LoggedInAs") %> <b><a href="<%: Url.Action("Index", "Account") %>"><%: HttpContext.Current.User.Identity.Name%></a></b>. 
    </div>
    <div>
    <span id="langContainer">
    <%= Html.ActionLink(IUDICO.LMS.Localization.getMessage("Language")+"  ", "ChangeCulture", "Account", new { lang = IUDICO.LMS.Localization.getMessage("ChangeCulture"), returnUrl = this.Request.RawUrl }, null)%>
    </span>            
    <span>
    <%: Html.ActionLink(IUDICO.LMS.Localization.getMessage("Logout"), "Logout", "Account") %>
    </span>
    </div>
<%
    }
    else
    {
%>


    <form action="<%: Url.Action("LoginDefault", "Account") %>" method="post">
        <input type="text" id="loginUsername" placeholder="<%=IUDICO.LMS.Localization.getMessage("Loginn") %>" name="loginUsername" size="40" />
        <input type="password" id="loginPassword" placeholder="<%=IUDICO.LMS.Localization.getMessage("Password") %>"  name="loginPassword" size="40" />

        <input type="submit" value="<%=IUDICO.LMS.Localization.getMessage("Ok") %>" class="okButton" />
    </form>

    <form action="<%: Url.Action("Login", "Account")  %>" method="post">
	    <input type="text" id="loginIdentifier" placeholder="<%=IUDICO.LMS.Localization.getMessage("OpenID") %>" name="loginIdentifier" size="40" />
	    
        <input type="submit" value="<%=IUDICO.LMS.Localization.getMessage("Ok")%>" class="okButton" />
	</form>
<%
    }
%>