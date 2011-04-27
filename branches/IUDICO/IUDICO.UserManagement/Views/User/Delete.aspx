<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.UserManagement.Localization.getMessage("Delete")%></h2>

    <h3><%=IUDICO.UserManagement.Localization.getMessage("YouWantDeleteThis")%>?</h3>
    <fieldset>
        <legend><%=IUDICO.UserManagement.Localization.getMessage("Fields")%></legend>
        
        <div class="display-label"><%=IUDICO.UserManagement.Localization.getMessage("Username") %></div>
        <div class="display-field"><%: Model.Username %></div>
        
        <div class="display-label"><%=IUDICO.UserManagement.Localization.getMessage("Name") %></div>
        <div class="display-field"><%: Model.Name %></div>
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("BackToList"), "Index")%>
        </p>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

