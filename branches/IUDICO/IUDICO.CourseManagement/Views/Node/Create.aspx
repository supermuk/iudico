<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Node>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=CourseManagRes.CourseManagement.Create %></h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <%= Html.EditorForModel() %>
            
            <p>
                <input type="submit" value=<%=CourseManagRes.CourseManagement.Create %> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink(CourseManagRes.CourseManagement.BackToList, "Index")%>
    </div>

</asp:Content>

