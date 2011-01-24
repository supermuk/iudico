<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateThemeModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create theme:</h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm("Create", "Theme"))
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
            <input type="submit" value="Create" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%: Html.ActionLink("Back to list", "Index", new { StageId = Model.StageId })%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
