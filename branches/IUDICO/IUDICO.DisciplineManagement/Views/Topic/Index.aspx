<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.DisciplineManagement.Models.ViewDataClasses.ViewTopicModel>>" %>

<%@ Assembly Name="IUDICO.DisciplineManagement" %>
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
                    alert("<%=IUDICO.DisciplineManagement.Localization.getMessage("PleaseSelectTopicsDelete") %>");

                    return false;
                }

                var answer = confirm("<%=IUDICO.DisciplineManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedTopics") %>");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/TopicAction/DeleteItems",
                    data: { topicIds: ids },
                    success: function (r) {
                        if (r.success) {
                            $("td input:checked").parents("tr").remove();
                        }
                        else {
                            alert("<%=IUDICO.DisciplineManagement.Localization.getMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> ");
                        }
                    }
                });
            });
        });
        function deleteItem(id) {
            var answer = confirm("<%=IUDICO.DisciplineManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedTopic") %>");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/TopicAction/DeleteItem",
                data: { topicId: id },
                success: function (r) {
                    if (r.success == true) {
                        var item = "item" + id;
                        $("tr[id=" + item + "]").remove();
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
    <%=IUDICO.DisciplineManagement.Localization.getMessage("Topics")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.DisciplineManagement.Localization.getMessage("TopicsFor")%>
    </h2>
    <h4>
        <%=ViewData["DisciplineName"]%><%=IUDICO.DisciplineManagement.Localization.getMessage("Next")%><%=ViewData["ChapterName"]%></h4>
    <p>
        <%: Html.ActionLink(IUDICO.DisciplineManagement.Localization.getMessage("AddTopic"), "Create")%>
        <a id="DeleteMany" href="#">
            <%=IUDICO.DisciplineManagement.Localization.getMessage("DeleteSelected")%></a>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                <%=IUDICO.DisciplineManagement.Localization.getMessage("TopicName")%>
            </th>
            <th>
                <%=IUDICO.DisciplineManagement.Localization.getMessage("Created")%>
            </th>
            <th>
                <%=IUDICO.DisciplineManagement.Localization.getMessage("Updated")%>
            </th>
            <th>
                <%=IUDICO.DisciplineManagement.Localization.getMessage("TestTopicType")%>
            </th>
            <th>
                <%=IUDICO.DisciplineManagement.Localization.getMessage("TestCourseName")%>
            </th>
            <th>
                <%=IUDICO.DisciplineManagement.Localization.getMessage("TheoryTopicType")%>
            </th>
            <th>
                <%=IUDICO.DisciplineManagement.Localization.getMessage("TheoryCourseName")%>
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
                <%: item.TopicName %>
            </td>
            <td>
                <%: item.Created %>
            </td>
            <td>
                <%: item.Updated %>
            </td>
            <td>
                <%: item.TestTopicType %>
            </td>
            <td>
                <%: item.TestCourseName %>
            </td>
            <td>
                <%: item.TheoryTopicType %>
            </td>
            <td>
                <%: item.TheoryCourseName%>
            </td>
            <td>
                <%: Html.ActionLink(IUDICO.DisciplineManagement.Localization.getMessage("Edit"), "Edit", new { TopicID = item.Id })%>
                |
                <%: Html.ActionLink(IUDICO.DisciplineManagement.Localization.getMessage("Up"), "TopicUp", new { TopicID = item.Id })%>
                |
                <%: Html.ActionLink(IUDICO.DisciplineManagement.Localization.getMessage("Down"), "TopicDown", new { TopicID = item.Id })%>
                | <a href="#" onclick="deleteItem(<%: item.Id %>)">
                    <%=IUDICO.DisciplineManagement.Localization.getMessage("Delete")%></a>
            </td>
        </tr>
        <% } %>
    </table>
    <div>
        <br />
       <%: Html.RouteLink(IUDICO.DisciplineManagement.Localization.getMessage("BackToChapters"), "Chapters", new { action = "Index", DisciplineId = ViewData["DisciplineId"] }, null)%>

    </div>
</asp:Content>
