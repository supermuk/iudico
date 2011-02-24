<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumAssignmentModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add Assignment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Add Assignment</h2>
    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm())
       {%>
        <%: Html.ValidationSummary(true, "Please correct the following error(s) and try again:")%>
        <fieldset>
            <legend>Fields</legend>
            <div class="editor-label">
                <%: Html.Label("Choose a group:") %>
            </div>
            <div>
                <%: Html.DropDownListFor(x => x.GroupId, Model.Groups)%>
            </div>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>
    <% } %>
    <div>
        <%: Html.ActionLink("Back to list", "Index") %>
    </div>
</asp:Content>
