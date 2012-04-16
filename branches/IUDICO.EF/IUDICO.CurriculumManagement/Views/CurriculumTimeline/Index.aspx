﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Timeline>>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("<%=IUDICO.CurriculumManagement.Localization.getMessage("PleaseSelectTimelineDelete") %>");

                    return false;
                }

                var answer = confirm("<%=IUDICO.CurriculumManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedTimelines") %>");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/CurriculumTimeline/DeleteItems",
                    data: { timelineIds: ids },
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
            var answer = confirm("<%=IUDICO.CurriculumManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedTimeline") %>");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/CurriculumTimeline/DeleteItem",
                data: { timelineId: id },
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
    <%=IUDICO.CurriculumManagement.Localization.getMessage("Timeline")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("CurriculumTimelinesFor")%>
    </h2>
    <h4>
        <%: (ViewData["Discipline"] as Discipline).Name%>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("PrevNext")%>
        <%: (ViewData["Group"] as IUDICO.Common.Models.Shared.Group).Name %>
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
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackCurriculums"), "Curriculums", new { action = "Index", DisciplineId = (ViewData["Discipline"] as Discipline).Id })%>
    </div>
</asp:Content>