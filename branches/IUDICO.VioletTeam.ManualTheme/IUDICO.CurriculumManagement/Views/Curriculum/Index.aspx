﻿<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Curriculum>>" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("<%=IUDICO.CurriculumManagement.Localization.getMessage("PleaseSelectCurriculumsDelete") %>");

                    return false;
                }

                var answer = confirm("<%=IUDICO.CurriculumManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedCurriculums") %>");

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
                        }
                        else {
                            alert("<%=IUDICO.CurriculumManagement.Localization.getMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                        }
                    }
                });
            });
        });
        function deleteItem(id) {
            var answer = confirm("<%=IUDICO.CurriculumManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedCurriculum") %>");

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
                        $("tr[id="+item+"]").remove();
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
    <%=IUDICO.CurriculumManagement.Localization.getMessage("Curriculums")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("Curriculums")%></h2>
    <p>
        <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("CreateNew"), "Create") %>
        <a id="DeleteMany" href="#"><%=IUDICO.CurriculumManagement.Localization.getMessage("DeleteSelected")%></a>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("Name") %>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("Created")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("Updated")%>
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
                    <%: item.Name %>
                </td>
                <td>
                    <%: String.Format("{0:g}", item.Created) %>
                </td>
                <td>
                    <%: String.Format("{0:g}", item.Updated) %>
                </td>
                <td>
                    <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("Edit"), "Edit", new { CurriculumID = item.Id })%>
                    |
                    <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("EditStages"), "Index", "Stage", new { CurriculumID = item.Id }, null)%>
                    |
                    <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("EditAssignments"),"Index","CurriculumAssignment", new { CurriculumID = item.Id }, null)%>
                    |
                    <a href="#" onclick="deleteItem(<%: item.Id %>)"><%=IUDICO.CurriculumManagement.Localization.getMessage("Delete")%></a>
                </td>
            </tr>
        <% } %>
    </table>
</asp:Content>