<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewCurriculumChapterModel>>" %>

<%@  Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CurriculumManagement.Localization.GetMessage("ChapterTimelines")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.GetMessage("ChapterTimelinesFor")%>
    </h2>
    <h4>
        <%: (ViewData["Discipline"] as Discipline).Name%>
        <% =IUDICO.CurriculumManagement.Localization.GetMessage("PrevNext")%>
        <%: ViewData["GroupName"] %>
    </h4>
    <table>
        <tr>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("Chapter")%>
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
        <% foreach (var item in Model)
           { %>
            <tr>
                <td>
                    <%: item.ChapterName %>
                </td>
                <td>
                    <%: String.Format("{0:g}", item.StartDate)%>
                </td>
                <td>
                    <%: String.Format("{0:g}", item.EndDate)%>
                </td>
                <td>
                    <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.GetMessage("Edit"), "Edit", new { CurriculumChapterId = item.Id }, null)%>
                    |
                    <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.GetMessage("EditCurriculumChapterTopics"), "Index", "CurriculumChapterTopic", new { CurriculumChapterId = item.Id }, null)%>
                </td>
            </tr>
        <% } %>
    </table>

    <div>
        <br />
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.GetMessage("BackCurriculums"), "Curriculums", new { action = "Index", DisciplineId = (ViewData["Discipline"] as Discipline).Id })%>
    </div>
</asp:Content>