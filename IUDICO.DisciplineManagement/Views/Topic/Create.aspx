﻿<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.DisciplineManagement.Models.ViewDataClasses.CreateTopicModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("CreateTopic") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Localization.GetMessage("CreateTopicFor") %></h2>
    <h4>
        <%=ViewData["DisciplineName"]%><%=Localization.GetMessage("Next")%><%=ViewData["ChapterName"]%>
    </h4>
    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm("Create", "Topic"))
       {%>
        <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>

        <fieldset>
            <legend><%=Localization.GetMessage("Fields")%></legend>

            <%= Html.EditorForModel() %>

<%--            <div class="editor-label">
                <%= Html.Label(Localization.getMessage("Name")) %>
            </div>
            <div class="editor-field">
                <%= Html.EditorFor(item => item.TopicName) %>
            </div>

            <div class="editor-label">
                <%: Html.Label(Localization.getMessage("ChooseCourseForTopic"))%>
            </div>
            <div>
                <%: Html.DropDownListFor(x => x.CourseId, Model.Courses)%>
            </div>
            <div class="editor-label">
                <%: Html.Label(Localization.getMessage("ChooseTopicType"))%>
            </div>
            <div>
                <%: Html.DropDownListFor(x => x.TopicTypeId, Model.TopicTypes)%>
            </div>--%>
        </fieldset>
        <p>
            <input type="submit" value="<%=Localization.GetMessage("Create") %>" />
        </p>
    <% } %>
    <div>
        <%: Html.ActionLink(Localization.GetMessage("BackToList"), "Index", new { ChapterId = Model.ChapterId })%>
    </div>
</asp:Content>