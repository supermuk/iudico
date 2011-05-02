<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewStageTimelineModel>>" %>

<%@  Assembly Name="IUDICO.CurriculumManagement" %>
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
                    url: "/StageTimeline/DeleteItems",
                    data: { timelineIds: ids },
                    success: function (r) {
                        if (r.success == true) {
                            $("td input:checked").parents("tr").remove();
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
                url: "/StageTimeline/DeleteItem",
                data: { timelineId: id },
                success: function (r) {
                    if (r.success == true) {
                        var item = "item" + id;
                        $("tr[id=" + item + "]").remove();
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
        <%=IUDICO.CurriculumManagement.Localization.getMessage("StageTimelinesFor")%>
    </h2>
    </h2>
    <h4>
        <%: (ViewData["Curriculum"] as Curriculum).Name%>
        <% =IUDICO.CurriculumManagement.Localization.getMessage("PrevNext")%>
        <%: (ViewData["Group"] as IUDICO.Common.Models.Group).Name %>
    </h4>
    <p>
        <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("AddTimeline"), "Create") %>
        <a id="DeleteMany" href="#"><%=IUDICO.CurriculumManagement.Localization.getMessage("DeleteSelected")%></a>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("Stage")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("StartDate")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("EndDate")%>
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
                    <%: item.StageName %>
                </td>
                <td>
                    <%: String.Format("{0:g}", item.StartDate)%>
                </td>
                <td>
                    <%: String.Format("{0:g}", item.EndDate)%>
                </td>
                <td>
                    <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("Edit"), "Edit", new { TimelineId = item.Id }, null)%>
                    |
                    <a href="#" onclick="deleteItem(<%: item.Id %>)"><%=IUDICO.CurriculumManagement.Localization.getMessage("Delete")%></a>
                </td>
            </tr>
        <% } %>
    </table>

    <div>
        <br />
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackCurriculumAssignments"), "CurriculumAssignments", new { action = "Index", CurriculumId = (ViewData["Curriculum"] as Curriculum).Id })%>
    </div>
</asp:Content>