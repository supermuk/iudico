<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.UserManagement.Localization.getMessage("CreateMultiple")%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.UserManagement.Localization.getMessage("CreateMultiple")%></h2>

    <% using (Html.BeginForm("CreateMultiple", "User", FormMethod.Post, new { enctype = "multipart/form-data" })) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend><%=IUDICO.UserManagement.Localization.getMessage("CreateMultiple")%></legend>
            
            <input type="file" id="FileCSV" name="fileUpload"/>
            
            <p>
                <input type="submit" value=<%=IUDICO.UserManagement.Localization.getMessage("Create")%> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("BackToList"), "Index")%>
    </div>

</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>