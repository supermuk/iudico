<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.EditCurriculumAssignmentTimelineModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Timeline
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create curriculum assignment timeline</h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        <%= Html.EditorFor(item=>item.Timeline) %>
        <div class="editor-label">
            <%: Html.Label("Choose an operation for timeline:") %>
        </div>
        <div>
            <%: Html.DropDownListFor(x => x.OperationId, Model.Operations)%>
        </div>
        <p>
            <input type="submit" value="Update" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <br />
        <%: Html.RouteLink("Back to list", "CurriculumAssignmentTimelines", new { action = "Index", CurriculumAssignmentId = HttpContext.Current.Session["CurriculumAssignmentId"] })%>
    </div>
</asp:Content>
