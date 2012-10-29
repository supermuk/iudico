<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.UserRoleModel>" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="IUDICO.Common" %>
<%@ Assembly Name="IUDICO.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("AddUsersToRole")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <h2><%=Localization.GetMessage("AddToRole")%></h2>

        <%:Html.ValidationSummary(true)%>
        
    <%
        using (Html.BeginForm())
        {%>
		  
        <fieldset>
            <legend><%=Localization.GetMessage("Fields")%></legend>
            
            <%= Html.EditorForModel() %>
            
            <p>
                <input type="submit" value=<%=Localization.GetMessage("Save")%> />
            </p>
        </fieldset>

    <%
        }%>

    <div>
        <%:Html.ActionLink(Localization.GetMessage("BackToList"), "Index")%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
