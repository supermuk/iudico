<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CourseMgt.Models.SearchResult.ISearchResult>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.CourseManagement.Localization.GetMessage("Search") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.CourseManagement.Localization.GetMessage("SearchResultsFor") %> "<%= ViewData["SearchString"] %>"</h2>

    <form action="/Search/Search" method="post">

        <%=IUDICO.CourseManagement.Localization.GetMessage("Search") %>:
        <%= Html.TextBox("query", ViewData["SearchString"])%>
        <input type="submit" value=<%=IUDICO.CourseManagement.Localization.GetMessage("Search") %> />

    </form>
    
    <ul>
    <% foreach (var result in Model) { %>   
        <li>#<%= result.GetID() %> <a href="<%= result.GetUrl() %>"><%= result.GetName() %></a><br />
            <%= result.GetText() %>
        </li>
    <% } %>
    </ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
