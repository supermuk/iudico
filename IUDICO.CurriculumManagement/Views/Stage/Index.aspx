﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Stage>>" %>
<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("Please select stages to delete");

                    return false;
                }

                var answer = confirm("Are you sure you want to delete " + ids.length + " selected stages?");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/Stage/DeleteItems",
                    data: { stageIds: ids },
                    success: function (r) {
                        if (r.success) {
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
            var answer = confirm("Are you sure you want to delete selected stage?");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/Stage/DeleteItem",
                data: { stageId: id },
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
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=CurriculumManagRes.CurriculumManagement.StagesFor%> <%: ViewData["CurriculumName"] %> curriculum.</h2>
    <p>
        <%: Html.ActionLink(CurriculumManagRes.CurriculumManagement.CreateNew, "Create")%>
        <a id="DeleteMany" href="#"><%=CurriculumManagRes.CurriculumManagement.DeleteSelected%></a>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                <%=CurriculumManagRes.CurriculumManagement.Name %>
            </th>
            <th>
                <%=CurriculumManagRes.CurriculumManagement.Created %>
            </th>
            <th>
                <%=CurriculumManagRes.CurriculumManagement.Updated %>
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
                    <%: Html.ActionLink(CurriculumManagRes.CurriculumManagement.Edit, "Edit", new { StageID = item.Id })%>
                    |
                    <%: Html.ActionLink(CurriculumManagRes.CurriculumManagement.EditThemes, "Index", "Theme", new { StageID = item.Id }, null)%>
                    |
                    <a href="#" onclick="deleteItem(<%: item.Id %>)">Delete</a>
                </td>
            </tr>
        <% } %>
    </table>

    <div>
        <br/>
        <%: Html.RouteLink("Back to curriculums.", "Curriculums", new { action = "Index" })%>
    </div>
</asp:Content>