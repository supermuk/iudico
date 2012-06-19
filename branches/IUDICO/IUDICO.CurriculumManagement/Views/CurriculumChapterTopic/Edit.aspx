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

    <span class="headerName"><%=Localization.GetMessage("Chapter")%>:</span>
    <span class="headerValue"><%: ViewData["ChapterName"] %></span>
    <span class="headerName"><%=Localization.GetMessage("Discipline")%>:</span>
    <span class="headerName"><%=Localization.GetMessage("Group")%>:</span>
    <span class="headerValue"><%: ViewData["GroupName"] %></span>

    <div class="backLink">
        <%: Html.RouteLink(Localization.GetMessage("BackTopicAssignmentList"), "CurriculumChapterTopics", new { action = "Index", CurriculumChapterId = HttpContext.Current.Session["CurriculumChapterId"] })%>
    </div>

    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
    <% Html.RenderPartial("EditorForCurriculumChapterTopicModel", Model); %>
    <p>
        <input type="submit" value="<%=Localization.GetMessage("Update")%>" />
    </p>
    <% } %>


</asp:Content>
