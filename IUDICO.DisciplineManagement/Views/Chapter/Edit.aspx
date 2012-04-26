<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.Chapter>" %>
<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Import Namespace="IUDICO.Common" %>
<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("EditChapter")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("EditChapter")%></h2>
    <% Html.EnableClientValidation(); %>
    <h4><%=ViewData["DisciplineName"]%><%=Localization.GetMessage("Next")%><%=Model.Name%></h4>
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true, Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        
        <fieldset>
            <legend><%=Localization.GetMessage("Fields")%></legend>
            
            <%= Html.EditorForModel()%>
        </fieldset>
        <p>
            <input type="submit" value="<%=Localization.GetMessage("Update")%>" />
        </p>
    <% } %>

    <div>
        <br />
        <%: Html.RouteLink(Localization.GetMessage("BackToList"), "Chapters", new { action = "Index", DisciplineId = HttpContext.Current.Session["DisciplineId"] })%>
    </div>

</asp:Content>