<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.DetailsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.UserManagement.Localization.getMessage("Details")%></h2>

    <fieldset>
        <legend><%=IUDICO.UserManagement.Localization.getMessage("Fields")%></legend>
        
        <%: Html.DisplayForModel() %>
        
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
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("Edit"), "Edit")%>
    </div>

    <div>
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("ChangePassword"), "ChangePassword")%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
