<%@ Assembly Name="IUDICO.Search" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Search.Models.SearchResult.ISearchResult>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.Search.Localization.getMessage("SearchResults")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    

    <form action="/Search/Search" method="post">

        
        <%= Html.TextBox("query", ViewData["SearchString"])%>
        <input type="submit" value=<%=IUDICO.Search.Localization.getMessage("Search")%> />

    </form>
    <div style="">Кількість результатів: <%= ViewData["total"] %>. Час пошуку: <%= ViewData["score"] %>ms. </div>
    </br>
    <ul style="margin-bottom:0em;">
    <% foreach (var result in Model) { %>   
        <li ><h3><a href="<%= result.GetUrl() %>"><%= result.GetName() %></a></h3>
            <div><%= result.GetText() %></div>
        </li>
    <% } %>
    </ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
