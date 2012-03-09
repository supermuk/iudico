<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (HttpContext.Current.User.Identity.IsAuthenticated)
    {
%>
    <form action="/Search/SearchSimple" method="post">
        <%= Html.TextBox("query") %>
        <input type="submit" value='<%=IUDICO.LMS.Localization.getMessage("Search")%>' />
    </form>
<%
    }
%>