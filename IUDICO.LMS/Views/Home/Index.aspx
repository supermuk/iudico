<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dictionary<IUDICO.Common.Models.Plugin.IPlugin, IEnumerable<IUDICO.Common.Models.Action>>>" %>
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.LMS.Models.HomeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	IUDICO
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

    <% if (Model.TopicsDescriptions.Count() > 0) { %>
    <h4><%=IUDICO.LMS.Localization.getMessage("AvailableTopics") %></h4>
    <ul>
    <% foreach (var topicDescription in Model.TopicsDescriptions)
       { %>
        <li><%: Html.ActionLink(topicDescription.ToString(), "Play", "Training", new { Id = topicDescription.Topic.Id }, null)%></li>
    <% } %>
    </ul>
    <% } %>
</asp:Content>
