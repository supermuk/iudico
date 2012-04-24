<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.DetailsModel>" %>
<%@ Import Namespace="IUDICO.Common.Models" %>
<%@ Import Namespace="IUDICO.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("Account")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("Details")%></h2>

    <fieldset>
        <legend><%=Localization.GetMessage("Fields")%></legend>
        <%=Html.Image("avatar", Model.Id, new {width = 100, height = 150})%>
        <%:Html.DisplayForModel()%>
        
    </fieldset>

    <fieldset>
        <legend><%=Localization.GetMessage("Roles")%></legend>

        <ul>
        
        <%
            foreach (var role in Model.Roles)
            {%>
            <li><%:Localization.GetMessage(role.ToString())%></li>
        <%
            }%>

        </ul>
    </fieldset>

    <fieldset>
        <legend><%=Localization.GetMessage("Groups")%></legend>

        <ul>
        
        <%
            foreach (var group in Model.Groups)
            {%>
            <li><%:group.Name%></li>
        <%
            }%>

        </ul>
    </fieldset>

    <div>
        <%:Html.ActionLink(Localization.GetMessage("Edit"), "Edit")%> |
        <%:Html.ActionLink(Localization.GetMessage("ChangePassword"), "ChangePassword")%>
        <% if (Roles.IsUserInRole(Role.Teacher.ToString()) && !Roles.IsUserInRole(Role.Admin.ToString())) { %>
            |
            <%:Html.ActionLink(Localization.GetMessage("UpgradeToAdmin"), "TeacherToAdminUpgrade")%>
        <% } %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
