<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.CurrMgt.Controllers.ThemeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Choose a course for theme:</h2>
    <% using (Html.BeginForm("Create", "Theme", new {StageId = Model.StageId, CourseID = Model.CourseId }))
       {%>
    <%: Html.ValidationSummary(true)%>
    <fieldset>
        <legend>Choose a course</legend>
        <%: Html.DropDownListFor(x => x.CourseId, Model.Courses)%>
        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%: Html.ActionLink("Back to List", "Index", new { StageId = Model.StageId })%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
