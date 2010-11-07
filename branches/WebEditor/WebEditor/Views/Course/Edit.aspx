<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WebEditor.Models.Course>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Id) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Id) %>
                <%: Html.ValidationMessageFor(model => model.Id) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Name) %>
                <%: Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Owner) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Owner) %>
                <%: Html.ValidationMessageFor(model => model.Owner) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Created) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Created, String.Format("{0:g}", Model.Created)) %>
                <%: Html.ValidationMessageFor(model => model.Created) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Updated) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Updated, String.Format("{0:g}", Model.Updated)) %>
                <%: Html.ValidationMessageFor(model => model.Updated) %>
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

