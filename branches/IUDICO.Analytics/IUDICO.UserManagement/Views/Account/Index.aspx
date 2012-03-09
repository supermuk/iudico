<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.DetailsModel>" %>
<%@ Import Namespace="IUDICO.Common.Models" %>
<%@ Import Namespace="IUDICO.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.getMessage("Account")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.getMessage("Details")%></h2>

    <fieldset>
        <legend><%=Localization.getMessage("Fields")%></legend>
        <%=Html.Image("avatar", Model.Id, new {width = 100, height = 150})%>
        <%:Html.DisplayForModel()%>
        
    </fieldset>

    <fieldset>
        <legend><%=Localization.getMessage("Roles")%></legend>

        <ul>
        
        <%
            foreach (var role in Model.Roles)
            {%>
            <li><%:Localization.getMessage(role.ToString())%></li>
        <%
            }%>

        </ul>
    </fieldset>

    <fieldset>
        <legend><%=Localization.getMessage("Groups")%></legend>

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
        <%:Html.ActionLink(Localization.getMessage("Edit"), "Edit")%>|
        <%:Html.ActionLink(Localization.getMessage("ChangePassword"), "ChangePassword")%>
        <%
            if (Roles.IsUserInRole(Role.Teacher.ToString()) && !Roles.IsUserInRole(Role.Admin.ToString()))
            {%>
        |
        <%:Html.ActionLink(Localization.getMessage("UpgradeToAdmin"), "TeacherToAdminUpgrade",
                                                  new {id = Model.Id})%>
        <%
            }%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
