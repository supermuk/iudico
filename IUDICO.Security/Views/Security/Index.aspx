<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Security.IndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.GetMessage("Security")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Model.GetMessage("Ban / Unban") %></h2>
    <ul>
        <li> <%= Html.ActionLink(Model.GetMessage("Ban / Unban"), "Index", "Ban") %> </li>
    </ul>
    <h2><%= Model.GetMessage("UserActivity") %></h2>
    <ul>
        <li><%= Html.ActionLink(Model.GetMessage("Overall stats"), "Overall", "UserActivity") %></li>
    </ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
