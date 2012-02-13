<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.TopicFeature>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.TopicId) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.TopicId) %>
                <%: Html.ValidationMessageFor(model => model.TopicId) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.FeatureId) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.FeatureId) %>
                <%: Html.ValidationMessageFor(model => model.FeatureId) %>
            </div>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Html.ResolveUrl("/Scripts/jquery/jquery.transfer.js")%>" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function removeRow(data) {
            window.location = window.location;
        }
    </script>
</asp:Content>

