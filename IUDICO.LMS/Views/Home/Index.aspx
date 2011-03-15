<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dictionary<IUDICO.Common.Models.Plugin.IPlugin, IEnumerable<IUDICO.Common.Models.Action>>>" %>
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.LMS.Models.HomeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Welcome to Butterfly - Web Editor for SCORM compatible courses.</h3>

    <% foreach (var plugin in Model.Actions) { %>
        <h4><%= plugin.Key.GetName() %></h4>
        <ul>
        <% foreach (var item in plugin.Value) { %>
            <li><a href="<%= item.Link %>"><%= item.Name %></a></li>
        <% } %>
        </ul>
    <% } %>

    <h4>Available themes:</h4>
    <ul>
    <% foreach (var theme in Model.AvailableThemes) { %>
        <li><%: Html.ActionLink(theme.Name, "Play", "Training", new { Id = theme.Id}, null) %></li>
    <% } %>
    </ul>
</asp:Content>
