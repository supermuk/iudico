﻿<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Group>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.UserManagement.Localization.getMessage("EditGroup")%> <%= Model.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.UserManagement.Localization.getMessage("EditGroup")%> <%= Model.Name %></h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(IUDICO.UserManagement.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        
        <fieldset>
            <legend><%=IUDICO.UserManagement.Localization.getMessage("Fields")%></legend>
            
            <%: Html.EditorForModel() %>
            
            <p>
                <input type="submit" value=<%=IUDICO.UserManagement.Localization.getMessage("Save")%> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("BackToList"), "Index")%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

