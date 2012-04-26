<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumModel>" %>

<%@  Assembly Name="IUDICO.CurriculumManagement" %>

<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("EditCurriculum")%> 
    <%--<%: ViewData["DisciplineName"]%>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("EditCurriculumFor")%></h2>
    <h4><%: ViewData["DisciplineName"]%></h4>
    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        <% Html.RenderPartial("EditorForCurriculumModel", Model); %>
        <p>
            <input type="submit" value="<%=Localization.GetMessage("Update") %>" />
        </p>
    <% } %>

    <div>
        <br />
        <%: Html.RouteLink(Localization.GetMessage("BackToList"), "Curriculums", new { action = "Index" })%>
    </div>
</asp:Content>

