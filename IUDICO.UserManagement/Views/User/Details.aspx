<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.AdminDetailsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        
        <%: Html.DisplayForModel() %>
        
    </fieldset>

    <fieldset>
        <legend>Groups</legend>

        <table>
        <tr>
            <th>
                Name
            </th>
            <th></th>
        </tr>
        
        <% foreach(var group in Model.Groups) { %>
        <tr>
            <td><%: group.Name %></td>
            <td><%: Html.ActionLink("Remove", "RemoveFromGroup", new { id = Model.Id, groupRef = group.Id })%></td>
        </tr>
        <% } %>

        </table>
    </fieldset>

    <p>
        <% if (Model.IsApproved)
            { %>
            <%: Html.ActionLink("Deactivate", "Deactivate", new { id = Model.Id })%> |
        <% }
            else
            { %>
            <%: Html.ActionLink("Activate", "Activate", new { id = Model.Id })%> |
        <% } %>
        <%: Html.ActionLink("Edit", "Edit", new { id=Model.Id }) %> |
        <%: Html.ActionLink("Delete", "Delete", new { id=Model.Id }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

