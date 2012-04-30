<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.GroupUser>" %>
<%@ Import Namespace="IUDICO.UserManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("AddUserToGroup")%> <%=Model.Group.Name%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("AddUserToGroup")%> <%=Model.Group.Name%></h2>

    <%
        using (Html.BeginForm())
        {%>
        <%:Html.ValidationSummary(true)%>
        
        <fieldset>
            <legend><%=Localization.GetMessage("Fields")%></legend>
            
            <%=Html.EditorForModel()%>
            
            <p>
                <input type="submit" value=<%=Localization.GetMessage("Add")%> />
            </p>
        </fieldset>

    <%
        }%>

    <div>
        <%:Html.ActionLink(Localization.GetMessage("BackToList"), "Index")%>
    </div>

</asp:Content>


