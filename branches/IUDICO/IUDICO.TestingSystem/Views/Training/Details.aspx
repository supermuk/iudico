<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.TestingSystem.Models.Shared.Training>" %>
<%@ Assembly Name="IUDICO.TestingSystem" %>
<%@ Assembly Name="Microsoft.LearningComponents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ViewPage1
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ViewPage1</h2>

    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">PackageId</div>
        <div class="display-field"><%: Model.PackageId %></div>
        
        <div class="display-label">PackageFileName</div>
        <div class="display-field"><%: Model.PackageFileName %></div>
        
        <div class="display-label">OrganizationId</div>
        <div class="display-field"><%: Model.OrganizationId %></div>
        
        <div class="display-label">OrganizationTitle</div>
        <div class="display-field"><%: Model.OrganizationTitle %></div>
        
        <div class="display-label">AttemptId</div>
        <div class="display-field"><%: Model.AttemptId %></div>
        
        <div class="display-label">UploadDateTime</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.UploadDateTime) %></div>
        
        <div class="display-label">TotalPoints</div>
        <div class="display-field"><%: Model.TotalPoints %></div>
        
        <div class="display-label">PlayId</div>
        <div class="display-field"><%: Model.PlayId %></div>
        
    </fieldset>
    <p>
        <%: Html.ActionLink("Edit", "Edit", new { /* id=Model.PrimaryKey */ }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

