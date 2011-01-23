<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewCurriculumAssignmentModel>>" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("Please select assignment to delete");

                    return false;
                }

                var answer = confirm("Are you sure you want to delete " + ids.length + " selected assignments?");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/CurriculumAssignment/DeleteItems",
                    data: { curriculumAssignmentIds: ids },
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
            var answer = confirm("Are you sure you want to delete selected assignment?");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/CurriculumAssignment/DeleteItem",
                data: { curriculumAssignmentId: id },
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
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Groups
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Assignments for
        <%: ViewData["CurriculumName"]%>
        curriculum
    </h2>
    <p>
        <%: Html.ActionLink("Add assignment", "Create") %>
        <a id="DeleteMany" href="#">Delete Selected</a>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                Id
            </th>
            <th>
                Group
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr id="item<%: item.Id %>">
            <td>
                <input type="checkbox" id="<%= item.Id %>" />
            </td>
            <td>
                <%: item.Id %>
            </td>
            <td>
                <%: item.GroupName %>
            </td>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { CurriculumAssignmentId = item.Id }, null)%>
                |
                <%: Html.ActionLink("Edit timelines", "Index", "CurriculumAssignmentTimeline", new { CurriculumAssignmentId = item.Id }, null)%>
                |
                <%: Html.ActionLink("Edit timelines for stages", "Index", "StageTimeline", new { CurriculumAssignmentId = item.Id }, null)%>
                |
                <a onclick="deleteItem(<%: item.Id %>)" href="#">Delete</a>
            </td>
        </tr>
        <% } %>
    </table>

    <div>
        <br/>
        <%: Html.RouteLink("Back to curriculums.", "Curriculums", new { action = "Index" })%>
    </div>
</asp:Content>
