<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewCurriculumChapterModel>>" %>

<%@  Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function onRowClick(id) {
            window.location.replace("/CurriculumChapter/" + id + "/CurriculumChapterTopic/Index");
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("ChapterTimelines")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Localization.GetMessage("ChapterTimelinesFor")%>
    </h2>

    <span class="headerName"><%: Localization.GetMessage("Discipline")%>:</span>
    <span class="headerValue"><%: ViewData["DisciplineName"] %></span>
    <span class="headerName"><%: Localization.GetMessage("Group")%>:</span>
    <span class="headerValue"><%: ViewData["GroupName"] %></span>

    <div class="backLink">
        <%: Html.RouteLink(Localization.GetMessage("BackCurriculums"), "Curriculums", new { action = "Index", DisciplineId = (ViewData["Discipline"] as Discipline).Id })%>
    </div>

    <table>
        <tr>
            <th>
                <%=Localization.GetMessage("Chapter")%>
            </th>
            <th>
                <%=Localization.GetMessage("StartDate")%>
            </th>
            <th>
                <%=Localization.GetMessage("EndDate")%>
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
            <tr>
                <td class="clickable" onclick="onRowClick(<%: item.Id %>);">
                    <%: item.ChapterName %>
                </td>
                <td class="clickable" onclick="onRowClick(<%: item.Id %>);">
                    <%: String.Format("{0:g}", item.StartDate)%>
                </td>
                <td class="clickable" onclick="onRowClick(<%: item.Id %>);">
                    <%: String.Format("{0:g}", item.EndDate)%>
                </td>
                <td>
                    <%: Html.ActionLink(Localization.GetMessage("Edit"), "Edit", new { CurriculumChapterId = item.Id }, null)%>
                    |
                    <%: Html.ActionLink(Localization.GetMessage("EditTopicAssignment"), "Index", "CurriculumChapterTopic", new { CurriculumChapterId = item.Id }, null)%>
                </td>
            </tr>
        <% } %>
    </table>

</asp:Content>