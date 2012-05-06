<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="IUDICO.Common" %>

<%
    if (HttpContext.Current.User.Identity.IsAuthenticated)
    {
%>
    <div>
    <%=Localization.GetMessage("LoggedInAs") %> <b><a href="<%: Url.Action("Index", "Account") %>"><%: HttpContext.Current.User.Identity.Name%></a></b>. 
    </div>
    <div>
    <span id="langContainer">
    <%= Html.ActionLink(Localization.GetMessage("Language")+"  ", "ChangeCulture", "Account", new { lang = Localization.GetMessage("ChangeCulture"), returnUrl = this.Request.RawUrl }, null)%>
    </span>            
    <span>
    <%: Html.ActionLink(Localization.GetMessage("Logout"), "Logout", "Account") %>
    </span>
    </div>
<%
    }
    else
    {
%>


    <form action="<%: Url.Action("LoginDefault", "Account", new {ReturnUrl = ViewContext.HttpContext.Request.QueryString["ReturnUrl"]}) %>" method="post">
        <input type="text" id="loginUsername" placeholder="<%=Localization.GetMessage("Loginn") %>" name="loginUsername" size="40" />
        <input type="password" id="loginPassword" placeholder="<%=Localization.GetMessage("Password") %>"  name="loginPassword" size="40" />

        <input id="loginDefaultButton" type="submit" value="<%=Localization.GetMessage("Ok") %>" class="okButton" />
    </form>

    <form action="<%: Url.Action("Login", "Account", new {ReturnUrl = ViewContext.HttpContext.Request.QueryString["ReturnUrl"]})  %>" method="post">
	    <input type="text" id="loginIdentifier" placeholder="<%=Localization.GetMessage("OpenID") %>" name="loginIdentifier" size="40" />
	    
        <input id="loginOpenIdButton" type="submit" value="<%=Localization.GetMessage("Ok")%>" class="okButton" />
	</form>
    <span id="langContainer">
    <%= Html.ActionLink(Localization.GetMessage("Language")+"  ", "ChangeCulture", "Account", new { lang = Localization.GetMessage("ChangeCulture"), returnUrl = this.Request.RawUrl }, null)%>
    </span>
    <span>
        <%: Html.ActionLink(Localization.GetMessage("Register"), "Register", "Account") %>
    </span>
    <span>
        <%: Html.ActionLink(Localization.GetMessage("ForgotPassword"), "Forgot", "Account")%>
    </span>
<%
    }
%>