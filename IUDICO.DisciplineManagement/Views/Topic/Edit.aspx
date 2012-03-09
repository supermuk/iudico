<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.DisciplineManagement.Models.ViewDataClasses.CreateTopicModel>" %>

<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.DisciplineManagement.Localization.getMessage("EditTopic") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=IUDICO.DisciplineManagement.Localization.getMessage("EditTopic") %></h2>
    <h4>
        <%=ViewData["DisciplineName"]%><%=IUDICO.DisciplineManagement.Localization.getMessage("Next")%><%=ViewData["ChapterName"]%>
        <%=IUDICO.DisciplineManagement.Localization.getMessage("Next")%><%=ViewData["TopicName"]%>
    </h4>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("Edit", "Topic"))
       {%>

        <%: Html.ValidationSummary(true, IUDICO.DisciplineManagement.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

        <fieldset>
            <legend><%=IUDICO.DisciplineManagement.Localization.getMessage("Fields")%></legend>
            <%= Html.EditorForModel() %>
   
<%--        <div class="editor-label">
            <%= Html.Label(IUDICO.DisciplineManagement.Localization.getMessage("Name")) %>
        </div>
        <div class="editor-field">
            <%= Html.EditorFor(item => item.TopicName) %>
        </div>

        <div class="editor-label">
            <%= Html.Label(IUDICO.DisciplineManagement.Localization.getMessage("ChooseCourseForTopic")) %>
        </div>
        <div>
            <%= Html.DropDownListFor(x => x.CourseId, Model.Courses)%>
        </div>
        <div class="editor-label">
            <%= Html.Label(IUDICO.DisciplineManagement.Localization.getMessage("ChooseTopicType"))%>
        </div>
        <div>
            <%= Html.DropDownListFor(x => x.TopicTypeId, Model.TopicTypes)%>
        </div>--%>
    </fieldset>
    <p>
        <input type="submit" value="<%=IUDICO.DisciplineManagement.Localization.getMessage("Update") %>" />
    </p>
    <% } %>
    <div>
        <%: Html.RouteLink(IUDICO.DisciplineManagement.Localization.getMessage("BackToList"), "Topics", new { action = "Index", ChapterId = Model.ChapterId })%>
    </div>
</asp:Content>
