<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Discipline>>" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("<%=IUDICO.DisciplineManagement.Localization.getMessage("PleaseSelectDisciplinesDelete") %>");

                    return false;
                }

                var answer = confirm("<%=IUDICO.DisciplineManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedDisciplines") %>");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/Discipline/DeleteItems",
                    data: { disciplineIds: ids },
                    success: function (r) {
                        if (r.success == true) {
                            $("td input:checked").parents("tr").remove();
                        }
                        else {
                            alert("<%=IUDICO.DisciplineManagement.Localization.getMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                        }
                    }
                });
            });
        });
        function deleteItem(id) {
            var answer = confirm("<%=IUDICO.DisciplineManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedDiscipline") %>");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/Discipline/DeleteItem",
                data: { disciplineId: id },
                success: function (r) {
                    if (r.success == true) {
                        var item = "item" + id;
                        $("tr[id="+item+"]").remove();
                    }
                    else {
                        alert("<%=IUDICO.DisciplineManagement.Localization.getMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                    }
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.DisciplineManagement.Localization.getMessage("Disciplines")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.DisciplineManagement.Localization.getMessage("Disciplines")%></h2>
    <p>
        <%: Html.ActionLink(IUDICO.DisciplineManagement.Localization.getMessage("CreateNew"), "Create") %>
        <a id="DeleteMany" href="#"><%=IUDICO.DisciplineManagement.Localization.getMessage("DeleteSelected")%></a>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                <%=IUDICO.DisciplineManagement.Localization.getMessage("Name") %>
            </th>
            <th>
                <%=IUDICO.DisciplineManagement.Localization.getMessage("Created")%>
            </th>
            <th>
                <%=IUDICO.DisciplineManagement.Localization.getMessage("Updated")%>
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
                    <%: Html.ActionLink(IUDICO.DisciplineManagement.Localization.getMessage("Edit"), "Edit", new { DisciplineID = item.Id })%>
                    |
                    <%: Html.ActionLink(IUDICO.DisciplineManagement.Localization.getMessage("EditChapters"), "Index", "Chapter", new { DisciplineID = item.Id }, null)%>
<%--                    |
                    <%: Html.ActionLink(IUDICO.DisciplineManagement.Localization.getMessage("EditCurriculums"), "Index", "Curriculum", new { DisciplineID = item.Id }, null)%>
--%>                    |
                    <a href="#" onclick="deleteItem(<%: item.Id %>)"><%=IUDICO.DisciplineManagement.Localization.getMessage("Delete")%></a>
                </td>
            </tr>
        <% } %>
    </table>
</asp:Content>