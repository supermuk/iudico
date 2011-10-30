<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateThemeModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CurriculumManagement.Localization.getMessage("CreateTheme") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("CreateThemeFor") %></h2>
    <h4>
        <%=ViewData["CurriculumName"]%><%=IUDICO.CurriculumManagement.Localization.getMessage("Next")%><%=ViewData["StageName"]%>
    </h4>
    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm("Create", "Theme"))
       {%>
        <%: Html.ValidationSummary(true, IUDICO.CurriculumManagement.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

        <fieldset>
            <legend><%=IUDICO.CurriculumManagement.Localization.getMessage("Fields")%></legend>

            <div class="editor-label">
                <%--<%= Html.LabelFor(item => item.ThemeName) %>--%>
                <%= Html.Label(IUDICO.CurriculumManagement.Localization.getMessage("Name")) %>
            </div>
            <div class="editor-field">
                <%= Html.EditorFor(item => item.ThemeName) %>
            </div>

            <div class="editor-label">
                <%: Html.Label(IUDICO.CurriculumManagement.Localization.getMessage("ChooseThemeType"))%>
                <%--<%= Html.LabelFor(item => item.ThemeTypeId) %>--%>
            </div>
            <div>
                <%: Html.DropDownListFor(x => x.ThemeTypeId, Model.ThemeTypes)%>
            </div>

            <div class="editor-label">
                <%: Html.Label(IUDICO.CurriculumManagement.Localization.getMessage("ChooseCourseForTheme"))%>
                <%--<%= Html.LabelFor(item => item.CourseId) %>--%>
            </div>
            <div>
                <%: Html.DropDownListFor(x => x.CourseId, Model.Courses)%>
            </div>
         
        </fieldset>
        <p>
            <input type="submit" value="<%=IUDICO.CurriculumManagement.Localization.getMessage("Create") %>" />
        </p>
    <% } %>
    <div>
        <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("BackToList"), "Index", new { StageId = Model.StageId })%>
    </div>
</asp:Content>