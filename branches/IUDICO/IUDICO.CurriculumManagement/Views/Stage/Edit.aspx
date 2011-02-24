<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Stage>" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit stage</h2>
    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true, "Please correct the following error(s) and try again:")%>
        
        <fieldset>
            <legend>Fields</legend>
            
            <%= Html.EditorForModel()%>
            
            <p>
                <input type="submit" value="Update" />
            </p>
        </fieldset>
    <% } %>

    <div>
        <br />
        <%: Html.RouteLink("Back to list", "Stages", new { action = "Index", CurriculumId = HttpContext.Current.Session["CurriculumId"] })%>
    </div>

</asp:Content>