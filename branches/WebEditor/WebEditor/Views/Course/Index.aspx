<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<WebEditor.Models.Course>>" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return this.id
                });
                if (ids.length == 0) {
                    alert("Please select courses to delete");
                    return false;
                }
                var answer = confirm("Are you sure you want to delete " + ids.length + " selected courses?");
                if (answer) {
                    $.ajax({
                        type: "post",
                        url: "DeleteMany",
                        data: { courseIds: ids },
                        success: function (r) {
                            if (r.success) {
                                $("td input:checked").parents("tr").remove();
                            }
                            else {
                                alert("Error occured during proccessing request");
                            }
                        }
                    });
                }
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
     <p>
        
        <%: Html.ActionLink("Create New", "Create") %> |
        <a id="DeleteMany" href="#">Delete Selected</a>
    </p>
    <table>
        <tr>
            <th></th>
            <th>
                Id
            </th>
            <th>
                Name
            </th>
            <th>
                Owner
            </th>
            <th>
                Created
            </th>
            <th>
                Updated
            </th>
            <th></th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <input type="checkbox" id="<%= item.Id %>" />
            </td>
            <td>
                <%: item.Id %>
            </td>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.Owner %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Created) %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Updated) %>
            </td>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { CourseID = item.Id })%> |
                <%: Html.ActionLink("Details", "Index", "Node", new { CourseID = item.Id}, null) %> |
                <%: Ajax.ActionLink("Delete", "Delete", new { CourseID = item.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + item.Name + "\"?", HttpMethod = "Delete" })%>
            </td>
        </tr>
    
    <% } %>

    </table>



</asp:Content>

