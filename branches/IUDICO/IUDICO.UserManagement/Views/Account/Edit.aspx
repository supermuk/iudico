﻿<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.EditModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=UsManagRes.UserManagement.Edit %></h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary("Correct the following error(s) and try again:") %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <%: Html.EditorForModel() %>
            
            <p>
                <input type="submit" value=<%=UsManagRes.UserManagement.Save %> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to Account", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
