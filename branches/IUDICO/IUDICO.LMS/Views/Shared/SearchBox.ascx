<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="IUDICO.Common" %>
<%
    if (HttpContext.Current.User.Identity.IsAuthenticated)
    {
%>
    <form action="/Search/SearchSimple" method="post">
        <%= Html.TextBox("SearchText", "", new { placeholder = Localization.GetMessage("Search") })%>
        <input type="submit" value='<%=Localization.GetMessage("Ok")%>' class="okButton" />
    </form>
<%
    }
%>