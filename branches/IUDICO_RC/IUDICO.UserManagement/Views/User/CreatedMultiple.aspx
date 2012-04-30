<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.Dictionary<System.String, System.String>>" %>
<%@ Import Namespace="IUDICO.UserManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("CreatedMultiple")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("CreatedMultiple")%></h2>

    <%=Localization.GetMessage("CreatedFollowingUsers")%>:

    <table width="100%">
        <thead>
            <tr>
                <th><%=Localization.GetMessage("Username")%></th>
                <th><%=Localization.GetMessage("Password")%></th>
            </tr>
        </thead>

        <tbody>
            <%
                foreach (var kv in Model)
                {%>
            <tr>
                <td><%=kv.Key%></td>
                <td><%=kv.Value%></td>
            </tr>
            <%
                }%>
        </tbody>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
