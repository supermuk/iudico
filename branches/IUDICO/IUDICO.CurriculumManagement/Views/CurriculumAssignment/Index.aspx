<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Group>>" %>

<%--<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
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
                    url: "/Curriculum/DeleteItems",
                    data: { curriculumIds: ids },
                    success: function (r) {
                        if (r.success == true) {
                            $("td input:checked").parents("tr").remove();
                            alert("Items were successfully deleted.");
                        }
                        else {
                            alert("Error occured during processing request.\nError message: " + r.message);
                        }
                    }
                });
            });
        });
        function deleteItem(id) {
            var answer = confirm("Are you sure you want to delete selected curriculum?");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/Curriculum/DeleteItem",
                data: { curriculumId: id },
                success: function (r) {
                    if (r.success == true) {
                        var item = "item" + id;
                        $("tr[id=" + item + "]").remove();
                        alert("Item was successfully deleted.");
                    }
                    else {
                        alert("Error occured during processing request.\nError message: " + r.message);
                    }
                }
            });
        }
    </script>
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Groups
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Groups</h2>
    <p>
        <%: Html.ActionLink("Add Group", "Add") %>
        <%--<a id="DeleteMany" href="#">Delete Selected</a>--%>
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
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
            <tr id="item<%: item.ID %>">
                <td>
                    <input type="checkbox" id="<%= item.ID %>" />
                </td>
                <td>
                    <%: item.ID %>
                </td>
                <td>
                    <%: item.Name %>
                </td>
                <td>
                    <%: Html.ActionLink("Edit Timeline", "EditTimeline", new { GroupID = item.ID })%>
                    |
                    <%: Html.ActionLink("Edit Timeline for Stages", "EditTimelineForStages", new { GroupID = item.ID }, null)%>
<%--                    |
                    <a href="javascript:deleteItem(<%: item.Id %>)">Delete</a>--%>
                </td>
            </tr>
        <% } %>
    </table>
</asp:Content>
