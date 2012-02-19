<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.Chapter>" %>
<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.CurriculumManagement.Localization.getMessage("Create")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.CurriculumManagement.Localization.getMessage("CreateChapterFor")%></h2>
    <h4><%: ViewData["DisciplineName"] %></h4>
    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true, IUDICO.CurriculumManagement.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        <fieldset>
            <legend><%=IUDICO.CurriculumManagement.Localization.getMessage("Fields")%></legend>
            
            <%= Html.EditorForModel() %>
        </fieldset>
        <p>
            <input type="submit" value="<%=IUDICO.CurriculumManagement.Localization.getMessage("Create")%>" />
        </p>
    <% } %>

    <div>
        <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("BackToList"), "Index")%>
    </div>

</asp:Content>