<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.CourseManagement.Localization.getMessage("Search") %></h2>
    
    <form action="/Search/Search" method="post">

        Search:
        <%= Html.TextBox("query") %>
         <input type="submit" value=<%=IUDICO.CourseManagement.Localization.getMessage("Search") %> />

    </form>

    <form action="/Search/Process" method="post">

         <input type="submit" value="Index" />

    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
