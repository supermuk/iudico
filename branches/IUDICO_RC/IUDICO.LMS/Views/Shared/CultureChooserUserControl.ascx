<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="IUDICO.Common" %>
<%= Html.ActionLink(Localization.GetMessage("Language")+"  ", "ChangeCulture", "Account", 
   new { lang = Localization.GetMessage("ChangeCulture"), returnUrl = this.Request.RawUrl }, null)%>