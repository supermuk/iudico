<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="IUDICO.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.getMessage("CreateMultiple")%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.getMessage("CreateMultiple")%></h2>

    <%
        using (Html.BeginForm("CreateMultiple", "User", FormMethod.Post, new {enctype = "multipart/form-data"}))
        {%>
        <%:Html.ValidationSummary(true)%>

        <fieldset>
            <legend><%=Localization.getMessage("CreateMultiple")%></legend>
            
            <input type="file" id="FileCSV" name="fileUpload"/>
            
            <p>
                <input type="submit" value=<%=Localization.getMessage("Create")%> />
            </p>
        </fieldset>

    <%
        }%>

    <div>
        <%:Html.ActionLink(Localization.getMessage("BackToList"), "Index")%>
    </div>

</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>