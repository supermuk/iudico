<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.ThemeAssignment>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common.Models" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Theme Assignment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit theme assignment
    </h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        <%= Html.EditorForModel() %>
        
        <p>
            <input type="submit" value="Update" />
        </p>
    </fieldset>
    <% } %>

    <div>
        <br />
        <%: Html.ActionLink("Back to theme assignment list", "ThemeAssignment")%>
    </div>
</asp:Content>
