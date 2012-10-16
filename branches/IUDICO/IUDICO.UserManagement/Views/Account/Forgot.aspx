<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.RestorePasswordModel>" %>
<%@ Import Namespace="IUDICO.UserManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("RestorePassword")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("RestorePassword")%></h2>

    <% using (Html.BeginForm()) {%>
        <%:Html.ValidationSummary(false)%>

        <fieldset>
            <legend><%=Localization.GetMessage("Fields")%></legend>
            
            <%:Html.EditorForModel()%>
            
            <p>
                <input type="submit" value=<%=Localization.GetMessage("RestorePassword")%> />
            </p>
        </fieldset>

    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

