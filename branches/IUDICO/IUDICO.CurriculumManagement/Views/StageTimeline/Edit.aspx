<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.EditStageTimelineModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit stage timeline
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit stage timeline</h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        <%: Html.EditorFor(item => item.Timeline) %>
        <div class="editor-label">
            <%: Html.Label("Choose an operation for timeline:") %>
        </div>
        <div>
            <%: Html.DropDownListFor(x => x.OperationId, Model.Operations)%>
        </div>
        <div class="editor-label">
            <%: Html.Label("Choose a stage for timeline:")%>
        </div>
        <div>
            <%: Html.DropDownListFor(x => x.StageId, Model.Stages)%>
        </div>
        <p>
            <input type="submit" value="Update" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <br />
        <%: Html.RouteLink("Back to list", "StageTimelines", new { action = "Index", CurriculumAssignmentId = HttpContext.Current.Session["CurriculumAssignmentId"] })%>
    </div>
</asp:Content>

