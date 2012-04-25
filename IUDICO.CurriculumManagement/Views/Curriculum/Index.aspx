<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewCurriculumModel>>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#curriculumsTable").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                iDisplayLength: 200,
                "bSort": true,
                "bLengthChange": false,
                "aoColumns": [
                null,
                null,
                null,
                null,
                null,
                { "bSortable": false }
                ]                                
            });
            
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("<%=IUDICO.CurriculumManagement.Localization.GetMessage("PleaseSelectCurriculumDelete") %>");

                    return false;
                }

                var answer = confirm("<%=IUDICO.CurriculumManagement.Localization.GetMessage("AreYouSureYouWantDeleteSelectedCurriculums") %>");

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
                            alert("<%=IUDICO.CurriculumManagement.Localization.GetMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                        }
                    }
                });
            });
        });
        function deleteItem(id) {
            var answer = confirm("<%=IUDICO.CurriculumManagement.Localization.GetMessage("AreYouSureYouWantDeleteSelectedCurriculum") %> ");

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
                    }
                    else {
                        alert("<%=IUDICO.CurriculumManagement.Localization.GetMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                    }
                }
            });
        }
        
        function details(id) {
            window.location.replace("/Curriculum?Details?id=" + id);
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CurriculumManagement.Localization.GetMessage("Curriculums")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.GetMessage("Curriculums")%>
    </h2>
    <p>
        <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.GetMessage("AddCurriculum"), "Create") %>
        <a id="DeleteMany" href="#"><%=IUDICO.CurriculumManagement.Localization.GetMessage("DeleteSelected")%></a>
    </p>
    <table id="curriculumsTable">
        <thead>
        <tr>
            <th>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("Group")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("Discipline")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("StartDate")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("EndDate")%>
            </th>
            <th>
            </th>
        </tr>
        </thead>
        <tbody>
        <% foreach (var item in Model)
           { %>
        <tr id="item<%: item.Id %>">
            <td>
                <input type="checkbox" id="<%= item.Id %>" />
            </td>
            <td onclick="details(<%: item.Id %>);">
                <%: item.GroupName %>
            </td>
            <td  onclick="details(<%: item.Id %>);">
                <%: item.DisciplineName %>
            </td>
            <td  onclick="details(<%: item.Id %>);">
                <%: item.StartDate %>
            </td>
            <td>
                <%: item.EndDate %>
            </td>
            <td>
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.GetMessage("Edit"), "Edit", new { CurriculumId = item.Id }, null)%>
                |
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.GetMessage("EditCurriculumChapters"), "Index", "CurriculumChapter", new { CurriculumId = item.Id }, null)%>
                |
                <a onclick="deleteItem(<%: item.Id %>)" href="#"><%=IUDICO.CurriculumManagement.Localization.GetMessage("Delete")%></a>
            </td>
        </tr>
        <% } %>
        </tbody>
    </table>

<%--    <div>
        <br/>
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackToDisciplines"), "Disciplines", new { action = "Index" })%>
    </div>--%>
</asp:Content>
