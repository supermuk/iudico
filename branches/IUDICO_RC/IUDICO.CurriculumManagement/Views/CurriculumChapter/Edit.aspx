<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumChapterModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("EditChapterTimelineFor")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Localization.GetMessage("EditChapterTimeline")%>
    </h2>

    <span class="headerName"><%: Localization.GetMessage("Discipline")%>:</span>
    <span class="headerValue"><%: ViewData["DisciplineName"] %></span>
    <span class="headerName"><%: Localization.GetMessage("Group")%>:</span>
    <span class="headerValue"><%: ViewData["GroupName"] %></span>
    
    <div class="backLink">
        <%: Html.RouteLink(Localization.GetMessage("BackToList"), "CurriculumChapters", new { action = "Index", CurriculumId = HttpContext.Current.Session["CurriculumId"] })%>
    </div>

    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
        <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        <% Html.RenderPartial("EditorForCurriculumChapterModel", Model); %>
        <p>
            <input type="submit" value="<%=Localization.GetMessage("Update")%>" />
        </p>
    <% } %>

</asp:Content>

