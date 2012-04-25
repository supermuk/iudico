<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumChapterModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CurriculumManagement.Localization.GetMessage("EditChapterTimelineFor")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.GetMessage("EditChapterTimeline")%>
    </h2>
    <h4>
        <%: ViewData["DisciplineName"]%>
        <%= IUDICO.CurriculumManagement.Localization.GetMessage("PrevNext")%>
        <%: ViewData["GroupName"] %>
    </h4>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
        <%: Html.ValidationSummary(true, IUDICO.CurriculumManagement.Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        <% Html.RenderPartial("EditorForCurriculumChapterModel", Model); %>
        <p>
            <input type="submit" value="<%=IUDICO.CurriculumManagement.Localization.GetMessage("Update")%>" />
        </p>
    <% } %>
    <div>
        <br />
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.GetMessage("BackToList"), "CurriculumChapters", new { action = "Index", CurriculumId = HttpContext.Current.Session["CurriculumId"] })%>
    </div>
</asp:Content>

