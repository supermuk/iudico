﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.CurrMgt.Controllers.ThemeModel>" %>

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
                    alert("Please select courses to delete");

                    return false;
                }

                var answer = confirm("Are you sure you want to delete " + ids.length + " selected courses?");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/Theme/Delete",
                    data: { themeIds: ids },
                    success: function (r) {
                        if (r.success) {
                            $("td input:checked").parents("tr").remove();
                        }
                        else {
                            alert("Error occured during proccessing request");
                        }
                    }
                });
            });

        });
        function removeRow(data) {
            window.location = window.location;
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Themes(Courses)</h2>

    <p>
        <%: Html.ActionLink("Add course", "Create", new { StageID = Model.StageId})%>
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
        <% foreach (var item in Model.Themes)
           { %>
        <tr>
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
                <%: Html.ActionLink("Edit", "Edit", new { ThemeID = item.Id/*, StageId = item.StageRef*/ })%>
                |
                <%: Html.ActionLink("Up", "ThemeUp", new { ThemeID = item.Id/*, StageId = item.StageRef*/})%>
                |
                <%: Html.ActionLink("Down", "ThemeDown", new { ThemeID = item.Id/*, StageId = item.StageRef*/ })%>
                |
                <%: Ajax.ActionLink("Delete", "Delete", new { ThemeID = item.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + item.Name + "\"?", HttpMethod = "Delete", OnSuccess = "removeRow" })%>
            </td>
        </tr>
        <% } %>
    </table>

    <div>
        <%: Html.ActionLink("Back to stage.", "Index", "Stage")%>
    </div>
</asp:Content>