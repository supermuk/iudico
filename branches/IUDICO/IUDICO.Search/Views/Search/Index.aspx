<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Assembly Name="IUDICO.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=IUDICO.Search.Localization.getMessage("Search")%> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    
    <form action="/Search/Search" method="post">

        
        <%= Html.TextBox("query") %>
         <input type="submit" value=<%=IUDICO.Search.Localization.getMessage("Search")%> />

    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
