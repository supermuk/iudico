<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateThemeModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit theme:</h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("Edit", "Theme"))
       {%>
    <%: Html.ValidationSummary(true)%>
    <fieldset>
        <legend>Fields</legend>

        <div class="editor-label">
            <%: Html.Label("Choose a course for theme:") %>
        </div>
        <div>
            <%: Html.DropDownListFor(x => x.CourseId, Model.Courses)%>
        </div>
        <div class="editor-label">
            <%: Html.Label("Choose a theme type:")%>
        </div>
        <div>
            <%: Html.DropDownListFor(x => x.ThemeTypeId, Model.ThemeTypes)%>
        </div>
        <p>
            <input type="submit" value="Update" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%: Html.RouteLink("Back to list", "Themes", new { action = "Index", StageId = Model.StageId })%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
