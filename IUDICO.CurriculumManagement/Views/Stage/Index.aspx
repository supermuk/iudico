<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Stage>>" %>

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
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Stages for <%: ViewData["CurriculumName"] %> curriculum.</h2>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
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
                Name
            </th>
            <th>
                Created
            </th>
            <th>
                Updated
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
                    <%: item.Name %>
                </td>
                <td>
                    <%: String.Format("{0:g}", item.Created) %>
                </td>
                <td>
                    <%: String.Format("{0:g}", item.Updated) %>
                </td>
                <td>
                    <%: Html.ActionLink("Edit", "Edit", new { StageID = item.Id })%>
                    |
                    <%: Html.ActionLink("Edit Themes", "Index", "Theme", new { StageID = item.Id }, null)%>
                    |
                    <a href="javascript:deleteItem(<%: item.Id %>)">Delete</a>
                </td>
            </tr>
        <% } %>
    </table>

    <div>
        <br/>
        <%: Html.RouteLink("Back to curriculums.", "Curriculums", new { action = "Index" })%>
    </div>
</asp:Content>