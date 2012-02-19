<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateTopicModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CurriculumManagement.Localization.getMessage("CreateTopic") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("CreateTopicFor") %></h2>
    <h4>
        <%=ViewData["DisciplineName"]%><%=IUDICO.CurriculumManagement.Localization.getMessage("Next")%><%=ViewData["ChapterName"]%>
    </h4>
    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm("Create", "Topic"))
       {%>
        <%: Html.ValidationSummary(true, IUDICO.CurriculumManagement.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

        <fieldset>
            <legend><%=IUDICO.CurriculumManagement.Localization.getMessage("Fields")%></legend>

            <div class="editor-label">
                <%--<%= Html.LabelFor(item => item.TopicName) %>--%>
                <%= Html.Label(IUDICO.CurriculumManagement.Localization.getMessage("Name")) %>
            </div>
            <div class="editor-field">
                <%= Html.EditorFor(item => item.TopicName) %>
            </div>

            <div class="editor-label">
                <%: Html.Label(IUDICO.CurriculumManagement.Localization.getMessage("ChooseCourseForTopic"))%>
                <%--<%= Html.LabelFor(item => item.CourseId) %>--%>
            </div>
            <div>
                <%: Html.DropDownListFor(x => x.CourseId, Model.Courses)%>
            </div>
            <div class="editor-label">
                <%: Html.Label(IUDICO.CurriculumManagement.Localization.getMessage("ChooseTopicType"))%>
                <%--<%= Html.LabelFor(item => item.TopicTypeId) %>--%>
            </div>
            <div>
                <%: Html.DropDownListFor(x => x.TopicTypeId, Model.TopicTypes)%>
            </div>
        </fieldset>
        <p>
            <input type="submit" value="<%=IUDICO.CurriculumManagement.Localization.getMessage("Create") %>" />
        </p>
    <% } %>
    <div>
        <%: Html.ActionLink(IUDICO.CurriculumManagement.Localization.getMessage("BackToList"), "Index", new { ChapterId = Model.ChapterId })%>
    </div>
</asp:Content>