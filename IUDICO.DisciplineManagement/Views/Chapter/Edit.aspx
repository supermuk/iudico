﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.Chapter>" %>
<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.DisciplineManagement.Localization.GetMessage("EditChapter")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.DisciplineManagement.Localization.GetMessage("EditChapter")%></h2>
    <% Html.EnableClientValidation(); %>
    <h4><%=ViewData["DisciplineName"]%><%=IUDICO.DisciplineManagement.Localization.GetMessage("Next")%><%=Model.Name%></h4>
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true, IUDICO.DisciplineManagement.Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        
        <fieldset>
            <legend><%=IUDICO.DisciplineManagement.Localization.GetMessage("Fields")%></legend>
            
            <%= Html.EditorForModel()%>
        </fieldset>
        <p>
            <input type="submit" value="<%=IUDICO.DisciplineManagement.Localization.GetMessage("Update")%>" />
        </p>
    <% } %>

    <div>
        <br />
        <%: Html.RouteLink(IUDICO.DisciplineManagement.Localization.GetMessage("BackToList"), "Chapters", new { action = "Index", DisciplineId = HttpContext.Current.Session["DisciplineId"] })%>
    </div>

</asp:Content>