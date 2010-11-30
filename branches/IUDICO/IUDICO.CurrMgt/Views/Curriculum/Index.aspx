<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Curriculum>>" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("Please select curriculums to delete");

                    return false;
                }

                var answer = confirm("Are you sure you want to delete " + ids.length + " selected curriculums?");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "Curriculum/Delete",
                    data: { curriculumIds: ids },
                    success: function (r) {
                        if (r.success == true) {
                            $("td input:checked").parents("tr").remove();
                        }
                        else {
                            alert("Error occured during proccessing request");
                        }
                    }
                });
            });
        });
        function removeRow(data) {
            window.location = window.location;
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Curriculums
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Curriculums</h2>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
        <a id="DeleteMany" href="">Delete Selected</a>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                Id
            </th>
            <th>
                Name
            </th>
            <th>
                Created
            </th>
            <th>
                Updated
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
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
                <%: String.Format("{0:g}", item.Created) %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Updated) %>
            </td>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { CurriculumID = item.Id })%>
                |
                <%: Html.ActionLink("Edit Stages", "Index", "Stage", new { CurriculumID = item.Id }, null)%>
                |
                <%: Ajax.ActionLink("Delete", "Delete", new { CurriculumID = item.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + item.Name + "\"?", HttpMethod = "Delete", OnSuccess="removeRow" })%>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>