<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    if (HttpContext.Current.User.Identity.IsAuthenticated)
    {
%>
    <div>
    <%=IUDICO.LMS.Localization.GetMessage("LoggedInAs") %> <b><a href="<%: Url.Action("Index", "Account") %>"><%: HttpContext.Current.User.Identity.Name%></a></b>. 
    </div>
    <div>
    <span id="langContainer">
    <%= Html.ActionLink(IUDICO.LMS.Localization.GetMessage("Language")+"  ", "ChangeCulture", "Account", new { lang = IUDICO.LMS.Localization.GetMessage("ChangeCulture"), returnUrl = this.Request.RawUrl }, null)%>
    </span>            
    <span>
    <%: Html.ActionLink(IUDICO.LMS.Localization.GetMessage("Logout"), "Logout", "Account") %>
    </span>
    </div>
<%
    }
    else
    {
%>


    <form action="<%: Url.Action("LoginDefault", "Account") %>" method="post">
        <input type="text" id="loginUsername" placeholder="<%=IUDICO.LMS.Localization.GetMessage("Loginn") %>" name="loginUsername" size="40" />
        <input type="password" id="loginPassword" placeholder="<%=IUDICO.LMS.Localization.GetMessage("Password") %>"  name="loginPassword" size="40" />

        <input id="loginDefaultButton" type="submit" value="<%=IUDICO.LMS.Localization.GetMessage("Ok") %>" class="okButton" />
    </form>

    <form action="<%: Url.Action("Login", "Account")  %>" method="post">
	    <input type="text" id="loginIdentifier" placeholder="<%=IUDICO.LMS.Localization.GetMessage("OpenID") %>" name="loginIdentifier" size="40" />
	    
        <input id="loginOpenIdButton" type="submit" value="<%=IUDICO.LMS.Localization.GetMessage("Ok")%>" class="okButton" />
	</form>
<%
    }
%>