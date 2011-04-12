<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.GroupUser>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=UsManagRes.UserManagement.AddUser%></h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <%= Html.EditorForModel() %>
            
            <p>
                <input type="submit" value=<%=UsManagRes.UserManagement.Save%> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink(UsManagRes.UserManagement.BackToList, "Index")%>
    </div>

</asp:Content>


