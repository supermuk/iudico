<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.Node>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.CourseManagement.Localization.GetMessage("Create") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.CourseManagement.Localization.GetMessage("Create") %></h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend><%=IUDICO.CourseManagement.Localization.GetMessage("Fields") %></legend>
            
            <%= Html.EditorForModel() %>
            
            <p>
                <input type="submit" value=<%=IUDICO.CourseManagement.Localization.GetMessage("Create") %> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink(IUDICO.CourseManagement.Localization.GetMessage("BackToList"), "Index")%>
    </div>

</asp:Content>

