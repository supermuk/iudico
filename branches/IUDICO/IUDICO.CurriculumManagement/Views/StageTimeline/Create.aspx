<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateStageTimelineModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create stage timeline
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("CreateStageTimelineFor")%>
    </h2>
    <h4>
        <%: ViewData["CurriculumName"]%>
        <%= IUDICO.CurriculumManagement.Localization.getMessage("PrevNext")%>
        <%: ViewData["GroupName"] %>
    </h4>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true, IUDICO.CurriculumManagement.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
    <fieldset>
        <legend><%=IUDICO.CurriculumManagement.Localization.getMessage("Fields")%></legend>
        <%: Html.EditorFor(item => item.Timeline) %>
        <div class="editor-label">
            <%: Html.Label(IUDICO.CurriculumManagement.Localization.getMessage("ChooseStageForTimeline"))%>
        </div>
        <div>
            <%: Html.DropDownListFor(x => x.StageId, Model.Stages)%>
        </div>
    </fieldset>
    <p>
        <input type="submit" value="<%=IUDICO.CurriculumManagement.Localization.getMessage("Create")%>" />
    </p>
    <% } %>
    <div>
        <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("BackToList"), "Index") %>
    </div>
</asp:Content>
