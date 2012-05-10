<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewCurriculumChapterTopicModel>>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("TopicAssignments")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Localization.GetMessage("TopicAssignmentsFor")%>
    </h2>

    <span class="headerName"><%=Localization.GetMessage("Chapter")%>:</span>
    <span class="headerValue"><%: ViewData["ChapterName"] %></span>
    <span class="headerName"><%=Localization.GetMessage("Discipline")%>:</span>
    <span class="headerValue"><%: Html.RouteLink(ViewData["DisciplineName"].ToString(), "CurriculumChapters", new { action = "Index", CurriculumId = ViewData["CurriculumId"] })%></span>
    <span class="headerName"><%=Localization.GetMessage("Group")%>:</span>
    <span class="headerValue"><%: ViewData["GroupName"] %></span>
    
    
    <div class="backLink">
        <%: Html.RouteLink(Localization.GetMessage("BackCurriculumChapters"), "CurriculumChapters", new { action = "Index", CurriculumId = ViewData["CurriculumId"] })%>
    </div>

    <table>
        <tr>
            <th>
                <%=Localization.GetMessage("TopicName")%>
            </th>
            <th>
                <%=Localization.GetMessage("MaxScore")%>
            </th>
            <th>
                <%=Localization.GetMessage("TestStartDate")%>
            </th>
            <th>
                <%=Localization.GetMessage("TestEndDate")%>
            </th>
            <th>
                <%=Localization.GetMessage("TheoryStartDate")%>
            </th>
            <th>
                <%=Localization.GetMessage("TheoryEndDate")%>
            </th>
            <th>
                <%=Localization.GetMessage("BlockTopicAtTesting")%>
            </th>
            <th>
                <%=Localization.GetMessage("BlockCurriculumAtTesting")%>
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
                <%: Html.ActionLink(Localization.GetMessage("Edit"), "Edit", new { CurriculumChapterTopicId = item.Id }, null)%>
            </td>
        </tr>
        <% } %>
    </table>

</asp:Content>
