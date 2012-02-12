<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.TopicAssignment>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CurriculumManagement.Localization.getMessage("EditTopicAssignment")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("EditTopicAssignmentFor")%>
    </h2>
    <h4>
        <%: ViewData["DisciplineName"]%>
        <%= IUDICO.CurriculumManagement.Localization.getMessage("PrevNext")%>
        <%: ViewData["GroupName"] %>
        <%= IUDICO.CurriculumManagement.Localization.getMessage("Next")%>
        <%: ViewData["ChapterName"] %>
    </h4>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true, IUDICO.CurriculumManagement.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
    <fieldset>
        <legend><%=IUDICO.CurriculumManagement.Localization.getMessage("Fields")%></legend>
        <%= Html.EditorForModel() %>
    </fieldset>
    <p>
        <input type="submit" value="<%=IUDICO.CurriculumManagement.Localization.getMessage("Update")%>" />
    </p>
    <% } %>

    <div>
        <%: Html.RouteLink(IUDICO.CurriculumManagement.Localization.getMessage("BackTopicAssignmentList"), "TopicAssignments", new { action = "Index", CurriculumId = HttpContext.Current.Session["CurriculumId"] })%>
    </div>
</asp:Content>
