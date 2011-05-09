<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dictionary<IUDICO.Common.Models.Plugin.IPlugin, IEnumerable<IUDICO.Common.Models.Action>>>" %>
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.LMS.Models.HomeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3><%=IUDICO.LMS.Localization.getMessage("WelcomeIudico")%></h3>

    <% foreach (var plugin in Model.Actions) { %>
        <% if (plugin.Value.Count() > 0) { %>
        <h4><%= plugin.Key.GetName() %></h4>
        <ul>
        <% foreach (var item in plugin.Value) { %>
            <li><a href="<%= item.Link %>"><%= item.Name %></a></li>
        <% } %>
        </ul>
        <% } %>
    <% } %>

    <% if (Model.ThemesDescriptions.Count() > 0) { %>
    <h4><%=IUDICO.LMS.Localization.getMessage("AvailableThemes") %></h4>
    <ul>
    <% foreach (var themeDescription in Model.ThemesDescriptions)
       { %>
        <li><%: Html.ActionLink(themeDescription.ToString(), "Play", "Training", new { Id = themeDescription.Theme.Id }, null)%></li>
    <% } %>
    </ul>
    <% } %>
</asp:Content>
