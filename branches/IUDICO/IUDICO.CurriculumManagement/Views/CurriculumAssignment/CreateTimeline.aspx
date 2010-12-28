<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.CreateTimelineModel>" %>
<%@  Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Timeline
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create timeline</h2>

    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            <div>
            <%: Html.LabelFor(item => item.timeline.StartDate) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(item => item.timeline.StartDate)%>
                <%: Html.ValidationMessageFor(item => item.timeline.StartDate, "*")%>
            </div>
            <div class="editor-label">
            <%: Html.LabelFor(item => item.timeline.EndDate)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(item => item.timeline.EndDate)%>
                <%: Html.ValidationMessageFor(item => item.timeline.EndDate, "*")%>
            </div>
            <div>
            <%: Html.DropDownListFor(x => x.OperationId,Model.Operations)%>

            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>
    <% } %>

    <div>
        <%: Html.ActionLink("Back to list", "EditTimeline", new { StageId = HttpContext.Current.Application["GroupId"] }, null)%>
    </div>

</asp:Content>