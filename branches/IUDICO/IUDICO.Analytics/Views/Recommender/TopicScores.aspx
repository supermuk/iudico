<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dictionary<int, IEnumerable<IUDICO.Common.Models.Shared.TopicScore>>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Topic Scores
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Topic Scores</h2>

     <table>
        <tr>
            <th><input name="checkall" type="checkbox" /></th>
            <th>Topic</th>
            <th>Scores</th>
        </tr>

     <% foreach (var item in Model) { %>
        <tr>
            <td>
                <input name="check[]" value="<%:item.Key%>" type="checkbox" />
            </td>
            <td>
                <%:item.Key%>
            </td>
            <td>
                <table>
                    <% foreach (var score in item.Value) { %>
                    <tr>
                        <td><%:score.TagId %></td>
                        <td><%:score.Score %></td>
                    </tr>
                    <% } %>
                </table>
            </td>
            <td>
                <%:Html.ActionLink("Update Topic Scores", "UpdateTopic", new { id = item.Key })%> |
            </td>
        </tr>
     <% } %>
     </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Html.ResolveUrl("/Scripts/Microsoft/MicrosoftAjax.js")%>" type="text/javascript"></script>
    <script src="<%=Html.ResolveUrl("/Scripts/Microsoft/MicrosoftMvcAjax.js")%>" type="text/javascript"></script>
</asp:Content>