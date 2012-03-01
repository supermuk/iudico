<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewCurriculumChapterTopicModel>>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CurriculumManagement.Localization.getMessage("TopicAssignments")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("TopicAssignmentsFor")%>
    </h2>
    <h4>
        <%: ViewData["DisciplineName"] %>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("PrevNext")%>
        <%: ViewData["GroupName"] %>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("Next")%>
        <%: ViewData["ChapterName"] %>
    </h4>
    <table>
        <tr>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("TopicName")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("MaxScore")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("TestStartDate")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("TestEndDate")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("TheoryStartDate")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("TheoryEndDate")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("BlockTopicAtTesting")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.getMessage("BlockCurriculumAtTesting")%>
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%: item.TopicName %>
            </td>
            <td>
                <%: item.MaxScore %>
            </td>
            <td>
                <%: item.TestStartDate %>
            </td>
            <td>
                <%: item.TestEndDate %>
            </td>
            <td>
                <%: item.TheoryStartDate %>
            </td>
            <td>
                <%: item.TheoryEndDate %>
            </td>
            <td>
                <%: item.BlockTopicAtTesting %>
            </td>
            <td>
                <%: item.BlockCurriculumAtTesting %>
            </td>
            <td>
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("Edit"), "Edit", new { CurriculumChapterTopicId = item.Id }, null)%>
            </td>
        </tr>
        <% } %>
    </table>
    <div>
        <br />
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackCurriculumChapters"), "CurriculumChapters", new { action = "Index", CurriculumId = ViewData["CurriculumId"] })%>
    </div>
</asp:Content>
