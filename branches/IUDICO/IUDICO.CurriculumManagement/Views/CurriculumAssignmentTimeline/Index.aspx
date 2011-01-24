<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Timeline>>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("Please select timeline to delete");

                    return false;
                }

                var answer = confirm("Are you sure you want to delete " + ids.length + " selected timelines?");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/CurriculumAssignmentTimeline/DeleteItems",
                    data: { timelineIds: ids },
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
            var answer = confirm("Are you sure you want to delete selected timeline?");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/CurriculumAssignmentTimeline/DeleteItem",
                data: { timelineId: id },
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
    Timeline
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Curriculum assignment timelines for
        <%: (ViewData["Curriculum"] as Curriculum).Name%>
        curriculum and
        <%: (ViewData["Group"] as IUDICO.Common.Models.Group).Name %>
        group
    </h2>
    <p>
        <%: Html.ActionLink("Add Timeline", "Create") %>
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
                Start Date
            </th>
            <th>
                End Date
            </th>
            <th>
                Operation
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
                <%: item.StartDate %>
            </td>
            <td>
                <%: item.EndDate %>
            </td>
            <td>
                <%: item.Operation.Name %>
            </td>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { TimelineId = item.Id }, null)%>
                |
                <a href="#" onclick="deleteItem(<%: item.Id %>)">Delete</a>
            </td>
        </tr>
        <% } %>
    </table>
    <div>
        <br />
        <%: Html.RouteLink("Back to curriculum assignments.", "CurriculumAssignments", new { action = "Index", CurriculumId = (ViewData["Curriculum"] as Curriculum).Id })%>
    </div>
</asp:Content>
