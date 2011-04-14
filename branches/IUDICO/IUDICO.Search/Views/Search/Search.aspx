<%@ Assembly Name="IUDICO.Search" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Search.Models.SearchResult.ISearchResult>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search Results
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Search results for "<%= ViewData["SearchString"] %>".</h2> 
    <p style="">Кількість результатів: <%= ViewData["total"] %>. Час пошуку: <%= ViewData["score"] %>ms. </p>

    <form action="/Search/Search" method="post">

        Search:
        <%= Html.TextBox("query", ViewData["SearchString"])%>
        <input type="submit" value="Search" />

    </form>
    
    <ul style="margin-bottom:0em;">
    <% foreach (var result in Model) { %>   
        <li style="background-color: #efefef;"><h3><a href="<%= result.GetUrl() %>"><%= result.GetName() %></a></h3>
            <p><%= result.GetText() %><p>
        </li>
    <% } %>
    </ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
