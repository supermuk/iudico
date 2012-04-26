<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.DisciplineManagement.Models.ViewDataClasses.ViewTopicModel>>" %>

<%@ Assembly Name="IUDICO.DisciplineManagement" %>

<%@ Import Namespace="IUDICO.Common" %>
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
                    alert("<%=Localization.GetMessage("PleaseSelectTopicsDelete") %>");

                    return false;
                }

                var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDeleteSelectedTopics") %>");

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
                            alert("<%=Localization.GetMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> ");
                        }
                    }
                });
            });
        });
        function deleteItem(id) {
            var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDeleteSelectedTopic") %>");

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
                        alert("<%=Localization.GetMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("Topics")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Localization.GetMessage("TopicsFor")%>
    </h2>
    <h4>
        <%=ViewData["DisciplineName"]%><%=Localization.GetMessage("Next")%><%=ViewData["ChapterName"]%></h4>
    <p>
        <%: Html.ActionLink(Localization.GetMessage("AddTopic"), "Create")%>
        <a id="DeleteMany" href="#">
            <%=Localization.GetMessage("DeleteSelected")%></a>
    </p>
    <table>
        <tr>
            <th>
            </th>
            <th>
                <%=Localization.GetMessage("TopicName")%>
            </th>
            <th>
                <%=Localization.GetMessage("Created")%>
            </th>
            <th>
                <%=Localization.GetMessage("Updated")%>
            </th>
            <th>
                <%=Localization.GetMessage("TestTopicType")%>
            </th>
            <th>
                <%=Localization.GetMessage("TestCourseName")%>
            </th>
            <th>
                <%=Localization.GetMessage("TheoryTopicType")%>
            </th>
            <th>
                <%=Localization.GetMessage("TheoryCourseName")%>
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
                <%: Html.ActionLink(Localization.GetMessage("Edit"), "Edit", new { TopicID = item.Id })%>
                |
                <%: Html.ActionLink(Localization.GetMessage("Up"), "TopicUp", new { TopicID = item.Id })%>
                |
                <%: Html.ActionLink(Localization.GetMessage("Down"), "TopicDown", new { TopicID = item.Id })%>
                | <a href="#" onclick="deleteItem(<%: item.Id %>)">
                    <%=Localization.GetMessage("Delete")%></a>
            </td>
        </tr>
        <% } %>
    </table>
    <div>
        <br />
       <%: Html.RouteLink(Localization.GetMessage("BackToChapters"), "Chapters", new { action = "Index", DisciplineId = ViewData["DisciplineId"] }, null)%>

    </div>
</asp:Content>
