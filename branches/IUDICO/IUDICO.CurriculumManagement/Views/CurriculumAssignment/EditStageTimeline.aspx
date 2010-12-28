<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Timeline>>" %>
<%@  Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
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
                    url: "/CurriculumAssignment/DeleteStageTimelineItems",
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
            var answer = confirm("Are you sure you want to delete selected assignment?");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/CurriculumAssignment/DeleteStageTimelineItem",
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
        Stage : <%: ViewData["StageName"] %>
    </h2>
    <p>
        <%: Html.ActionLink("Add Timeline", "CreateStageTimeline") %>
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
                    <% if (item.OperationRef == 1)  { %>
                        View
                    <% } %>
                    <% else  { %>
                        Pass
                    <% } %>
                </td>
                <td>
                    <a href="javascript:deleteItem(<%: item.Id %>)">Delete</a>
                </td>
            </tr>
        <% } %>
    </table>
    
<%--    <p>
    <%: Html.ActionLink("Back to stages", "EditTimelineForStages", new { GroupId = HttpContext.Current.Application["GroupId"] }, null)%>
    </p>--%>

</asp:Content>