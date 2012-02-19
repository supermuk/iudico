<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.Dictionary<System.String, System.String>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CreatedMultiple
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>CreatedMultiple</h2>

    Created following users:

    <table width="100%">
        <thead>
            <tr>
                <th>Username</th>
                <th>Password</th>
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
