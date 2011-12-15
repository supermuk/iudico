<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.String>" %>
<%@ Assembly Name="IUDICO.TestingSystem" %>
<%@ Import namespace="IUDICO.TestingSystem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Localization.getMessage("Error_View_Title")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Localization.getMessage("Error_View_Title")%></h2>
    <h4><%: Model.ToString() %></h4>
    <p><%: Html.ActionLink(Localization.getMessage("Return_Back"), "Index", "Home") %></p>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
