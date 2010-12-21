<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Theme>>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("Please select themes to delete");

                    return false;
                }

                var answer = confirm("Are you sure you want to delete " + ids.length + " selected themes?");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/Theme/DeleteItems",
                    data: { themeIds: ids },
                    success: function (r) {
                        if (r.success) {
                            $("td input:checked").parents("tr").remove();
                            alert("Items were successfully deleted.");
                        }
                        else {
                            alert("Error occured during proccessing request");
                        }
                    }
                });
            });
        });
        function deleteItem(id) {
            var answer = confirm("Are you sure you want to delete selected theme?");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/Theme/DeleteItem",
                data: { themeId: id },
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

    <h2>Themes(Courses) for <%: ViewData["StageName"] %> stage.</h2>

    <p>
        <%: Html.ActionLink("Add course", "Create"/*, new { StageID = Model.StageId}*/)%>
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
                Course name
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
                    <%: Html.ActionLink("Edit", "Edit", new { ThemeID = item.Id })%>
                    |
                    <%: Html.ActionLink("Up", "ThemeUp", new { ThemeID = item.Id })%>
                    |
                    <%: Html.ActionLink("Down", "ThemeDown", new { ThemeID = item.Id })%>
                    |
                    <a href="javascript:deleteItem(<%: item.Id %>)">Delete</a>
                </td>
            </tr>
        <% } %>
    </table>

    <div>
        <br />
       <%: Html.RouteLink("Back to stages.", "Stages", new { action = "Index", CurriculumId = ViewData["CurriculumId"] }, null)%>
    </div>
</asp:Content>