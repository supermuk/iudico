<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewTopicAssignmentModel>>" %>

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
        <%: (ViewData["Discipline"] as Discipline).Name%>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("PrevNext")%>
        <%: (ViewData["Group"] as IUDICO.Common.Models.Shared.Group).Name %>
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
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr id="item<%: item.TopicAssignment.Id %>">
            <td>
                <%: item.Topic.Name %>
            </td>
            <td>
                <%: item.TopicAssignment.MaxScore %>
            </td>
            <td>
                <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("Edit"), "Edit", new { TopicAssignmentId = item.TopicAssignment.Id }, null)%>
            </td>
        </tr>
        <% } %>
    </table>
    <div>
        <br />
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackCurriculums"), "Curriculums", new { action = "Index", DisciplineId = (ViewData["Discipline"] as Discipline).Id })%>
    </div>
</asp:Content>
