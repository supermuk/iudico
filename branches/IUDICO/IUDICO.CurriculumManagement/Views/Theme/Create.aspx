<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.CreateThemeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create theme:</h2>
    <% using (Html.BeginForm("Create", "Theme"))
       {%>
    <%: Html.ValidationSummary(true)%>
    <fieldset>
        <legend>Choose a course for theme</legend>
        <%: Html.DropDownListFor(x => x.CourseId, Model.Courses)%>
        <legend>Choose a theme type: </legend>
        <%: Html.DropDownListFor(x => x.ThemeTypeId, Model.ThemeTypes)%>
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
