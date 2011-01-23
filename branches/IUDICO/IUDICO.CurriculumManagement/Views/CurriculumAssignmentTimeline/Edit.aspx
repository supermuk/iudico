<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.CurriculumManagement.Models.CreateTimelineModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Timeline
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit timeline</h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        <%= Html.EditorFor(item=>item.Timeline) %>
        <div>
            <%: Html.DropDownListFor(x => x.OperationId,Model.Operations)%>
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
