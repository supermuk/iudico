<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=UsManagRes.UserManagem.Delete%></h2>

    <h3><%=UsManagRes.UserManagem.YouWantDeleteThis%>?</h3>
    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label"><%=UsManagRes.UserManagem.Username %></div>
        <div class="display-field"><%: Model.Username %></div>
        
        <div class="display-label"><%=UsManagRes.UserManagem.Name %></div>
        <div class="display-field"><%: Model.Name %></div>
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink(UsManagRes.UserManagem.BackToList, "Index")%>
        </p>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

