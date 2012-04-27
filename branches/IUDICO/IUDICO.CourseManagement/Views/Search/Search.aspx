<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CourseMgt.Models.SearchResult.ISearchResult>>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("Search") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("SearchResultsFor") %> "<%= ViewData["SearchString"] %>"</h2>

    <form action="/Search/Search" method="post">

        <%=Localization.GetMessage("Search") %>:
        <%= Html.TextBox("query", ViewData["SearchString"])%>
        <input type="submit" value=<%=Localization.GetMessage("Search") %> />

    </form>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
