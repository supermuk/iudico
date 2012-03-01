<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dictionary<IUDICO.Common.Models.Plugin.IPlugin, IEnumerable<IUDICO.Common.Models.Action>>>" %>
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.LMS.Models.HomeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	IUDICO
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    $(function () {
        $('.rating').rating({
            callback: function (value, link) {
                $(this).rating('readOnly');
                
                var id = $(this).attr('name').replace('rating_', '');


                $.post('/Account/RateTopic', { 'topicId': id, 'score': value }, function (data) {
                    
                });
            }
        });
    });
</script>
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
    <ul class="topics">
    <% foreach (var topicDescription in Model.TopicsDescriptions)
       { %>
        <li>
        <% for (var i = 1; i <= 5; ++i) { %>
            <input name="rating_<%=topicDescription.Topic.Id %>" value="<%= i %>" <%= (topicDescription.Rating == i ? "checked='checked'" : "") %> <%= (topicDescription.Rating != 0 ? "disabled='disabled'" : "") %> type="radio" class="rating required"/>
        <% } %>
        <%: Html.ActionLink(topicDescription.ToString(), "Play", "Training",
                                new { TopicId = topicDescription.Topic.Id, TopicPart = topicDescription.TopicPart, TopicType = topicDescription.TopicType }, null)%>
        </li>
    <% } %>
    </ul>
    <% } %>
</asp:Content>
