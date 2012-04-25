<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewCurriculumChapterTopicModel>>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CurriculumManagement.Localization.GetMessage("TopicAssignments")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.GetMessage("TopicAssignmentsFor")%>
    </h2>
    <h4>
        <%: ViewData["DisciplineName"] %>
        <%=IUDICO.CurriculumManagement.Localization.GetMessage("PrevNext")%>
        <%: ViewData["GroupName"] %>
        <%=IUDICO.CurriculumManagement.Localization.GetMessage("Next")%>
        <%: ViewData["ChapterName"] %>
    </h4>
    <table>
        <tr>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("TopicName")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("MaxScore")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("TestStartDate")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("TestEndDate")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("TheoryStartDate")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("TheoryEndDate")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("BlockTopicAtTesting")%>
            </th>
            <th>
                <%=IUDICO.CurriculumManagement.Localization.GetMessage("BlockCurriculumAtTesting")%>
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
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.GetMessage("Edit"), "Edit", new { CurriculumChapterTopicId = item.Id }, null)%>
            </td>
        </tr>
        <% } %>
    </table>
    <div>
        <br />
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.GetMessage("BackCurriculumChapters"), "CurriculumChapters", new { action = "Index", CurriculumId = ViewData["CurriculumId"] })%>
    </div>
</asp:Content>
