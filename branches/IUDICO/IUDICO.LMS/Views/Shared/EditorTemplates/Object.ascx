<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% if (ViewData.TemplateInfo.TemplateDepth > 1) { %>
    <%= Html.Editor(ViewData.ModelMetadata.PropertyName) %>
<% }
   else { %>   
    <% foreach (var prop in ViewData.ModelMetadata.Properties.Where(pm => pm.ShowForEdit
                         && !ViewData.TemplateInfo.Visited(pm))) { %>
        <% if (prop.HideSurroundingHtml) { %>
            <%= Html.Editor(prop.PropertyName) %>
        <% }
           else { %>
            <% if (!String.IsNullOrEmpty(Html.Label(prop.PropertyName).ToHtmlString())) { %>
                <div class="editor-label"><%= Html.Label(prop.PropertyName) %></div>
            <% } %>
            <div class="editor-field">
                <%= Html.Editor(prop.PropertyName) %>
                <%= Html.ValidationMessage(prop.PropertyName, "*") %>
            </div>
        <% } %>
    <% } %>
<% } %>