<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewCurriculumAssignmentModel>>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("<%=IUDICO.CurriculumManagement.Localization.getMessage("PleaseSelectAssignmentDelete") %>");

                    return false;
                }

                var answer = confirm("<%=IUDICO.CurriculumManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedAssignments") %>");

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
                        }
                        else {
                            alert("<%=IUDICO.CurriculumManagement.Localization.getMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                        }
                    }
                });
            });
        });
        function deleteItem(id) {
            var answer = confirm("<%=IUDICO.CurriculumManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedAssignment") %> ");

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
                    }
                    else {
                        alert("<%=IUDICO.CurriculumManagement.Localization.getMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CurriculumManagement.Localization.getMessage("AssignmentsFor")%> <%: ViewData["CurriculumName"]%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("AssignmentsFor")%>
    </h2>
    <h4>
        <%: ViewData["CurriculumName"]%>
    </h4>
    <p>
        <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("AddAssignment"), "Create") %>
        <a id="DeleteMany" href="#"><%=IUDICO.CurriculumManagement.Localization.getMessage("DeleteSelected")%></a>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("Group")%>
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
                <%: item.GroupName %>
            </td>
            <td>
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("Edit"), "Edit", new { CurriculumAssignmentId = item.Id }, null)%>
                |
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("EditTimelines"), "Index", "CurriculumAssignmentTimeline", new { CurriculumAssignmentId = item.Id }, null)%>
                |
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("EditTimelinesForStages"), "Index", "StageTimeline", new { CurriculumAssignmentId = item.Id }, null)%>
                |
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("EditThemeAssignments"), "Index", "ThemeAssignment", new { CurriculumAssignmentId = item.Id }, null)%>
                |
                <a onclick="deleteItem(<%: item.Id %>)" href="#"><%=IUDICO.CurriculumManagement.Localization.getMessage("Delete")%></a>
            </td>
        </tr>
        <% } %>
    </table>

    <div>
        <br/>
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackToCurriculums"), "Curriculums", new { action = "Index" })%>
    </div>
</asp:Content>
