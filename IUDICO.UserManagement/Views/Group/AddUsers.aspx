<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.GroupUser>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add User To Group <%= Model.Group.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.UserManagement.Localization.getMessage("AddUserToGroup")%> <%= Model.Group.Name %></h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend><%=IUDICO.UserManagement.Localization.getMessage("Fields")%></legend>
            
            <%= Html.EditorForModel() %>
            
            <p>
                <input type="submit" value=<%=IUDICO.UserManagement.Localization.getMessage("Add") %> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("BackToList"), "Index")%>
    </div>

</asp:Content>


