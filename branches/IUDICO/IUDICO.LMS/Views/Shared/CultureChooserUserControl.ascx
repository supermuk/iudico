<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%= Html.ActionLink(IUDICO.LMS.Localization.GetMessage("Language")+"  ", "ChangeCulture", "Account", 
   new { lang = IUDICO.LMS.Localization.GetMessage("ChangeCulture"), returnUrl = this.Request.RawUrl }, null)%>