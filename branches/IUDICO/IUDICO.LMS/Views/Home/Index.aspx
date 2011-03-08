<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dictionary<IUDICO.Common.Models.Plugin.IPlugin, IEnumerable<IUDICO.Common.Models.Action>>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Welcome to Butterfly - Web Editor for SCORM compatible courses.</h3>

    <% foreach (var plugin in Model) { %>
        <h4><%= plugin.Key.GetName() %></h4>
        <ul>
        <% foreach (var item in plugin.Value) { %>
            <li><a href="<%= item.Link %>"><%= item.Name %></a></li>
        <% } %>
        </ul>
    <% } %>

</asp:Content>
