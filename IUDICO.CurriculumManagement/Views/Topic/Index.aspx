<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Topic>>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
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
                    alert("<%=IUDICO.CurriculumManagement.Localization.getMessage("PleaseSelectTopicsDelete") %>");

                    return false;
                }

                var answer = confirm("<%=IUDICO.CurriculumManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedTopics") %>");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/Topic/DeleteItems",
                    data: { topicIds: ids },
                    success: function (r) {
                        if (r.success) {
                            $("td input:checked").parents("tr").remove();
                        }
                        else {
                            alert("<%=IUDICO.CurriculumManagement.Localization.getMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> ");
                        }
                    }
                });
            });
        });
        function deleteItem(id) {
            var answer = confirm("<%=IUDICO.CurriculumManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedTopic") %>");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/Topic/DeleteItem",
                data: { topicId: id },
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
    <%=IUDICO.CurriculumManagement.Localization.getMessage("Topics")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("TopicsFor")%>
    </h2>
    <h4>
        <%=ViewData["DisciplineName"]%><%=IUDICO.CurriculumManagement.Localization.getMessage("Next")%><%=ViewData["ChapterName"]%></h4>
    <p>
        <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("AddTopic"), "Create")%>
        <a id="DeleteMany" href="#">
            <%=IUDICO.CurriculumManagement.Localization.getMessage("DeleteSelected")%></a>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("TopicName")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("Created")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("Updated")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("TopicType")%>
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
                <%: IUDICO.CurriculumManagement.Converters.ConvertToString(item.TopicType) %>
            </td>
            <td>
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("Edit"), "Edit", new { TopicID = item.Id })%>
                |
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("Up"), "TopicUp", new { TopicID = item.Id })%>
                |
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("Down"), "TopicDown", new { TopicID = item.Id })%>
                | <a href="#" onclick="deleteItem(<%: item.Id %>)">
                    <%=IUDICO.CurriculumManagement.Localization.getMessage("Delete")%></a>
            </td>
        </tr>
        <% } %>
    </table>
    <div>
        <br />
       <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackToChapters"), "Chapters", new { action = "Index", DisciplineId = ViewData["DisciplineId"] }, null)%>

    </div>
</asp:Content>
