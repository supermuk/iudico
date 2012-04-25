<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumChapterTopicModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CurriculumManagement.Localization.GetMessage("EditTopicAssignment")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.GetMessage("EditTopicAssignmentFor")%>
    </h2>
    <h4>
        <%: ViewData["DisciplineName"]%>
        <%= IUDICO.CurriculumManagement.Localization.GetMessage("PrevNext")%>
        <%: ViewData["GroupName"] %>
        <%= IUDICO.CurriculumManagement.Localization.GetMessage("Next")%>
        <%: ViewData["ChapterName"] %>
        <%= IUDICO.CurriculumManagement.Localization.GetMessage("Next")%>
        <%: ViewData["TopicName"] %>
    </h4>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true, IUDICO.CurriculumManagement.Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
    <% Html.RenderPartial("EditorForCurriculumChapterTopicModel", Model); %>
    <p>
        <input type="submit" value="<%=IUDICO.CurriculumManagement.Localization.GetMessage("Update")%>" />
    </p>
    <% } %>

    <div>
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.GetMessage("BackTopicAssignmentList"), "CurriculumChapterTopics", new { action = "Index", CurriculumChapterId = HttpContext.Current.Session["CurriculumChapterId"] })%>
    </div>
</asp:Content>
