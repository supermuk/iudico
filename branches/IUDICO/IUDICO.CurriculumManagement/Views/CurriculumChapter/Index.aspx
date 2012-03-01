<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewCurriculumChapterModel>>" %>

<%@  Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CurriculumManagement.Localization.getMessage("ChapterTimelines")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("ChapterTimelinesFor")%>
    </h2>
    <h4>
        <%: (ViewData["Discipline"] as Discipline).Name%>
        <% =IUDICO.CurriculumManagement.Localization.getMessage("PrevNext")%>
        <%: ViewData["GroupName"] %>
    </h4>
    <table>
        <tr>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("Chapter")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("StartDate")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("EndDate")%>
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
                    <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("Edit"), "Edit", new { CurriculumChapterId = item.Id }, null)%>
                    |
                    <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("EditCurriculumChapterTopics"), "Index", "CurriculumChapterTopic", new { CurriculumChapterId = item.Id }, null)%>
                </td>
            </tr>
        <% } %>
    </table>

    <div>
        <br />
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackCurriculums"), "Curriculums", new { action = "Index", DisciplineId = (ViewData["Discipline"] as Discipline).Id })%>
    </div>
</asp:Content>