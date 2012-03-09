<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.User>" %>
<%@ Import Namespace="IUDICO.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.getMessage("Delete")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.getMessage("Delete")%></h2>

    <h3><%=Localization.getMessage("YouWantDeleteThis")%>?</h3>
    <fieldset>
        <legend><%=Localization.getMessage("Fields")%></legend>
        
        <div class="display-label"><%=Localization.getMessage("Username")%></div>
        <div class="display-field"><%:Model.Username%></div>
        
        <div class="display-label"><%=Localization.getMessage("Name")%></div>
        <div class="display-field"><%:Model.Name%></div>
    </fieldset>
    <%
        using (Html.BeginForm())
        {%>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%:Html.ActionLink(Localization.getMessage("BackToList"), "Index")%>
        </p>
    <%
        }%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

