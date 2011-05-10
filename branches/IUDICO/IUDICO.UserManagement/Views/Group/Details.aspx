<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Group>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details of Group <%: Model.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.UserManagement.Localization.getMessage("DetailsOfGroup")%> <%: Model.Name %></h2>

    <fieldset>
        <legend><%=IUDICO.UserManagement.Localization.getMessage("Users")%></legend>

        <table>
        <tr>
            <th>
                <%=IUDICO.UserManagement.Localization.getMessage("Username")%>
            </th>
            <th>
                <%=IUDICO.UserManagement.Localization.getMessage("Name")%>
            </th>
            <th></th>
        </tr>

        <% if (Model.GroupUsers.GetEnumerator().MoveNext())
           {
               foreach (var groupUser in Model.GroupUsers)
               { %>
            <tr>
                <td>
                    <%: groupUser.User.Username%>
                </td>
                <td>
                    <%: groupUser.User.Name%>
                </td>
                <td>
                    <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("RemoveUser"), "RemoveUser", new { id = Model.Id, userRef = groupUser.User.Id })%>
                </td>
            </tr>
        <% }
           }
           else
           { %>
            <tr>
                <td><%=IUDICO.UserManagement.Localization.getMessage("NoData")%></td>
                <td><%=IUDICO.UserManagement.Localization.getMessage("NoData")%></td>
                <td><%=IUDICO.UserManagement.Localization.getMessage("NoActions")%></td>
            </tr>
        <% } %>

        </table>

    </fieldset>
    <p>

        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("Edit"), "Edit", new { id = Model.Id })%> |
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("AddUser"), "AddUsers", new { id = Model.Id })%> |
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("BackToList"), "Index")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

