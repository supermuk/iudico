<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumChapterTopicModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("EditTopicAssignment")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Localization.GetMessage("EditTopicAssignmentFor")%>
    </h2>
    <h4>
        <%: ViewData["DisciplineName"]%>
        <%= Localization.GetMessage("PrevNext")%>
        <%: ViewData["GroupName"] %>
        <%= Localization.GetMessage("Next")%>
        <%: ViewData["ChapterName"] %>
        <%= Localization.GetMessage("Next")%>
        <%: ViewData["TopicName"] %>
    </h4>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
    <% Html.RenderPartial("EditorForCurriculumChapterTopicModel", Model); %>
    <p>
        <input type="submit" value="<%=Localization.GetMessage("Update")%>" />
    </p>
    <% } %>

    <div>
        <%: Html.RouteLink(Localization.GetMessage("BackTopicAssignmentList"), "CurriculumChapterTopics", new { action = "Index", CurriculumChapterId = HttpContext.Current.Session["CurriculumChapterId"] })%>
    </div>
</asp:Content>
