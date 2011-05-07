<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<IUDICO.Common.Models.NodeProperty>>" %>

    <% foreach (var item in Model) { %>
        <div>
            <%= Html.EditorForModel(item) %>
        </div>
    <% } %>

