<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Group>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th>
                <%=IUDICO.UserManagement.Localization.getMessage("Name")%>
            </th>
            <th></th>
        </tr>

    <% if (Model.GetEnumerator().MoveNext())
       {
           foreach (var item in Model)
           { %>
    
        <tr>
            <td>
                <%: item.Name%>
            </td>
            <td>
                <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("Edit"), "Edit", new { id = item.Id })%> |
                <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("Details"), "Details", new { id = item.Id })%> |
                <%: Ajax.ActionLink(IUDICO.UserManagement.Localization.getMessage("Delete"), "Delete", new { id = item.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + item.Name + "\"?", HttpMethod = "Delete", OnSuccess = "removeRow" })%>
            </td>
        </tr>
    
    <% }
       }
       else
       {%>
        <tr>
            <td>
                <%=IUDICO.UserManagement.Localization.getMessage("NoDate")%>
            </td>
            <td>
                <%=IUDICO.UserManagement.Localization.getMessage("NoActions")%>
            </td>
        </tr>
    <% } %>
    </table>

    <p>
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("CreateNew"), "Create")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script>
        function removeRow(result) {
            if (result) {
                document.location = document.location;
            }
            else {
                alert("Can delete selected group. It's active or doesn't exist.");
            }
        }
    </script>
</asp:Content>

