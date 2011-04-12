<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.DetailsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=UsManagRes.UserManagement.Details %></h2>

    <fieldset>
        <legend>Fields</legend>
        
        <%: Html.DisplayForModel() %>
        
    </fieldset>

    <fieldset>
        <legend><%=UsManagRes.UserManagement.Groups %></legend>

        <ul>
        
        <% foreach(var group in Model.Groups) { %>
            <li><%: group.Name %></li>
        <% } %>

        </ul>
    </fieldset>

    <div>
        <%: Html.ActionLink(UsManagRes.UserManagement.Edit, "Edit") %>
    </div>

    <div>
        <%: Html.ActionLink(UsManagRes.UserManagement.ChangePassword, "ChangePassword")%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
