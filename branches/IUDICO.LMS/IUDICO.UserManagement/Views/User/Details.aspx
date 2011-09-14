<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.AdminDetailsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.UserManagement.Localization.getMessage("DetailsOfUser")%> <%= Model.Username %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.UserManagement.Localization.getMessage("DetailsOfUser")%> <%= Model.Username %></h2>

    <fieldset>
        <legend><%=IUDICO.UserManagement.Localization.getMessage("Fields")%></legend>
        
        <%: Html.DisplayForModel() %>
        
    </fieldset>

    <fieldset>
        <legend><%=IUDICO.UserManagement.Localization.getMessage("Groups")%></legend>

        <table>
        <tr>
            <th>
                <%=IUDICO.UserManagement.Localization.getMessage("Name")%>
            </th>
            <th></th>
        </tr>
        
        <% if (Model.Groups.GetEnumerator().MoveNext())
           {
               foreach (var group in Model.Groups)
               { %>
        <tr>
            <td><%: group.Name%></td>
            <td><%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("Remove"), "RemoveFromGroup", new { id = Model.Id, groupRef = group.Id })%></td>
        </tr>
        <% }
           }
           else
           { %>
           <tr>
            <td><%=IUDICO.UserManagement.Localization.getMessage("NoData")%></td>
            <td><%=IUDICO.UserManagement.Localization.getMessage("NoActions")%></td>
           </tr>
           <% } %>

        </table>
    </fieldset>

    <p>
        <% if (Model.IsApproved)
            { %>
            <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("Deactivate"), "Deactivate", new { id = Model.Id })%> |
        <% }
            else
            { %>
            <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("Activate"), "Activate", new { id = Model.Id })%> |
        <% } %>
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("Edit"), "Edit", new { id = Model.Id })%> |
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("AddToGroup"), "AddToGroup", new { id = Model.Id })%> |
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("Delete"), "Delete", new { id = Model.Id })%> |
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("BackToList"), "Index")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

