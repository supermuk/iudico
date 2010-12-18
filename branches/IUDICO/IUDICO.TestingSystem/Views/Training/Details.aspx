<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.TestingSystem.Models.Shared.Training>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Training Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Details</h2>
    <fieldset>
        <legend>Records</legend>
        <div>
            <div class="display-label">
                Package ID</div>
            <div class="display-field">
                <%: Model.PackageID %></div>
        </div>
        <div>
            <div class="display-label">
                Package FileName</div>
            <div class="display-field">
                <%: Model.PackageFileName %></div>
        </div>
        <div>
            <div class="display-label">
                Organization ID</div>
            <div class="display-field">
                <%: Model.OrganizationID %></div>
        </div>
        <div>
            <div class="display-label">
                Organization Title</div>
            <div class="display-field">
                <%: Model.OrganizationTitle %></div>
        </div>
        <div>
            <div class="display-label">
                Attempt ID</div>
            <div class="display-field">
                <%: Model.AttemptID %></div>
        </div>
            <div>
            <div class="display-label">
                Attempt Status</div>
            <div class="display-field">
                <%: Model.AttemptStatusProp %></div>
        </div>
        <div>
            <div class="display-label">
                Upload Time</div>
            <div class="display-field">
                <%: String.Format("{0:g}", Model.UploadDateTime) %></div>
        </div>
        <div>
            <div class="display-label">
                Total Points</div>
            <div class="display-field">
                <%: Model.TotalPoints %></div>
        </div>
        <div>
            <div class="display-label">
                Play ID</div>
            <div class="display-field">
                <%: Model.PlayID %></div>
        </div>
    </fieldset>
    <p>
        <%: Model.AttemptStatusProp == null ? Html.ActionLink("Play", "Create", new { id = Model.PlayID }) : Html.ActionLink("Play", "Play", new { id = Model.PlayID })%>
        |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
