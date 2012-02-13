<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.Feature>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Topics to Feature <%= Model.Feature.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Topics to Feature <%= Model.Feature.Name %></h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <select multiple="multiple" id="availableTopics">
	            <% foreach (var topic in Model.AvailableTopics) { %>
                    <option value="<%= topic.Id %>"><%= topic.Name %></option>
                <% } %>/>
            </select>
            <a href="#" id="addTopic">add</a>

            <select multiple="multiple" id="selectedTopics">
                <% foreach (var topic in Model.Topics) { %>
                    <option value="<%= topic.Id %>"><%= topic.Name %></option>
                <% } %>/>
            </select>

            <a href="#" id="removeTopic">remove</a>
            
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
            $('#availableTopics').transfer({
                to: '#selectedTopics', //selector of second multiple select box
                addId: '#addTopic', //add buttong id
                removeId: '#removeTopic' // remove button id
	        });
        });
    </script>
</asp:Content>