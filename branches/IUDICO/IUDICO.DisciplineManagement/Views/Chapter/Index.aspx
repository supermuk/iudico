<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Chapter>>" %>
<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Import Namespace="IUDICO.Common" %>
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
                    alert("<%=Localization.GetMessage("PleaseSelectChaptersDelete") %>");

                    return false;
                }

                var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDeleteSelectedChapters") %>");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/ChapterAction/DeleteItems",
                    data: { chapterIds: ids },
                    success: function (r) {
                        if (r.success) {
                            $("td input:checked").parents("tr").remove();
                        }
                        else {
                            alert("<%=Localization.GetMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                        }
                    }
                });
            });
        });
        function deleteItem(id) {
            var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDeleteSelectedChapter") %>");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/ChapterAction/DeleteItem",
                data: { chapterId: id },
                success: function (r) {
                    if (r.success == true) {
                        var item = "item" + id;
                        $("tr[id=" + item + "]").remove();
                    }
                    else {
                        alert("<%=Localization.GetMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                    }
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("Chapters")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%=Localization.GetMessage("ChaptersFor")%></h2>
    <h4><%: ViewData["DisciplineName"] %></h4>
    <p>
        <%: Html.ActionLink(Localization.GetMessage("CreateNew"), "Create")%>
        <a id="DeleteMany" href="#"><%=Localization.GetMessage("DeleteSelected")%></a>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                <%=Localization.GetMessage("Name") %>
            </th>
            <th>
                <%=Localization.GetMessage("Created") %>
            </th>
            <th>
                <%=Localization.GetMessage("Updated") %>
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
                    <%: Html.ActionLink(Localization.GetMessage("Edit"), "Edit", new { ChapterID = item.Id })%>
                    |
                    <%: Html.ActionLink(Localization.GetMessage("EditTopics"), "Index", "Topic", new { ChapterID = item.Id }, null)%>
                    |
                    <a href="#" onclick="deleteItem(<%: item.Id %>)"><%=Localization.GetMessage("Delete") %></a>
                </td>
            </tr>
        <% } %>
    </table>

    <div>
        <br/>
        <%: Html.RouteLink(Localization.GetMessage("BackToDisciplines"), "Disciplines", new { action = "Index" })%>
    </div>
</asp:Content>