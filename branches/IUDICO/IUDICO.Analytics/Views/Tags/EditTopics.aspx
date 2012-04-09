<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Analytics.Models.ViewDataClasses.ViewTagDetails>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Topics to Tag #<%= Model.Tag.Id %> "<%= Model.Tag.Name %>"
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Topics to Tag #<%= Model.Tag.Id %> "<%= Model.Tag.Name%>"</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div>
                <select multiple="multiple" id="availableTopics">
	                <% foreach (var topic in Model.AvailableTopics) { %>
                        <option value="<%= topic.Id %>"><%= topic.Name %></option>
                    <% } %>
                </select>
                <select name="topics" multiple="multiple" id="selectedTopics">
                    <% foreach (var topic in Model.Topics) { %>
                        <option value="<%= topic.Id %>"><%= topic.Name %></option>
                    <% } %>
                </select>
            </div>
            <div>
                <a href="#" id="addTopic">add</a>
                <a href="#" id="removeTopic">remove</a>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Html.ResolveUrl("/Scripts/jquery/jquery.transfer.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('#addTopic').click(function (e) {
                $('#availableTopics option:selected').remove().appendTo('#selectedTopics');

                e.preventDefault();

                return false;
            });
            $('#removeTopic').click(function (e) {
                $('#selectedTopics option:selected').remove().appendTo('#availableTopics');

                e.preventDefault();

                return false;
            });

            $('#selectedTopics').closest('form').submit(function () {
                $('#selectedTopics option').each(function (i) {
                    $(this).attr("selected", "selected");
                });
            });
        });
    </script>
</asp:Content>