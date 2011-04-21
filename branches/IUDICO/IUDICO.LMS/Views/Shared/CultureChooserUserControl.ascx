<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%= Html.ActionLink(IUDICO.LMS.Localization.getMessage("Language")+"  ", "ChangeCulture", "Account", 
   new { lang = IUDICO.LMS.Localization.getMessage("ChangeCulture"), returnUrl = this.Request.RawUrl }, null)%>