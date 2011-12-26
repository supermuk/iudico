<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Import Namespace="IUDICO.Common.Models" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.DetailsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.UserManagement.Localization.getMessage("Account")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.UserManagement.Localization.getMessage("Details")%></h2>

    <fieldset>
        <legend><%=IUDICO.UserManagement.Localization.getMessage("Fields")%></legend>
        <%= Html.Image("avatar", Model.Id, new {width = 100, height = 150}) %>
        <%: Html.DisplayForModel() %>
        
    </fieldset>

    <fieldset>
        <legend><%=IUDICO.UserManagement.Localization.getMessage("Roles")%></legend>

        <ul>
        
        <% foreach(var role in Model.Roles) { %>
            <li><%: IUDICO.UserManagement.Localization.getMessage(role.ToString())%></li>
        <% } %>

        </ul>
    </fieldset>

    <fieldset>
        <legend><%=IUDICO.UserManagement.Localization.getMessage("Groups")%></legend>

        <ul>
        
        <% foreach(var group in Model.Groups) { %>
            <li><%: group.Name %></li>
        <% } %>

        </ul>
    </fieldset>

    <div>
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("Edit"), "Edit")%>|
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("ChangePassword"), "ChangePassword")%>
        <% if (Roles.IsUserInRole(Role.Teacher.ToString()) && !Roles.IsUserInRole(Role.Admin.ToString())) { %>
        |
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("UpgradeToAdmin"), "TeacherToAdminUpgrade", new { id = Model.Id })%>
        <% } %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
