<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%= Html.ActionLink("English", "ChangeCulture", "Account", 
   new { lang = "en", returnUrl = this.Request.RawUrl }, null)%>
<%= Html.ActionLink("Українська", "ChangeCulture", "Account", 
   new { lang = "uk", returnUrl = this.Request.RawUrl }, null)%>