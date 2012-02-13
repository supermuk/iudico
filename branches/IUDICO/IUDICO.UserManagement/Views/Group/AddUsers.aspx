<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.GroupUser>" %>
<%@ Import Namespace="IUDICO.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.getMessage("AddUserToGroup")%> <%=Model.Group.Name%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.getMessage("AddUserToGroup")%> <%=Model.Group.Name%></h2>

    <%
        using (Html.BeginForm())
        {%>
        <%:Html.ValidationSummary(true)%>
        
        <fieldset>
            <legend><%=Localization.getMessage("Fields")%></legend>
            
            <%=Html.EditorForModel()%>
            
            <p>
                <input type="submit" value=<%=Localization.getMessage("Add")%> />
            </p>
        </fieldset>

    <%
        }%>

    <div>
        <%:Html.ActionLink(Localization.getMessage("BackToList"), "Index")%>
    </div>

</asp:Content>


