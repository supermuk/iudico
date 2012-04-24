<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.EditComputersViewModel>" %>

<asp:Content ID="Content0" ContentPlaceHolderID="TitleContent" runat="server">
	EditComputer
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Computer</h2>

    <%= Html.EditorForModel(Model) %>
    <p>
        <input type="submit" value="Save" name="saveButton" />
    </p>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
