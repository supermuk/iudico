<%@ Assembly Name="IUDICO.UserManagement" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.Group>" %>

<%@ Import Namespace="IUDICO.UserManagement" %>
<%@ Import Namespace="IUDICO.Common" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $('#groupUsersTable').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                iDisplayLength: 50,
                "bSort": true,
                "aoColumns": [
                    null,
                    null,
                    { "bSortable": false }
                ]
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("DetailsOfGroup")%>
    <%:Model.Name%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Localization.GetMessage("StudentsOfGroup")%>
        <%:Model.Name%></h2>
    <p>
        <%:Html.ActionLink(Localization.GetMessage("Edit"), "Edit", new {id = Model.Id})%>
        |
        <%:Html.ActionLink(Localization.GetMessage("AddUser"), "AddUsers", new {id = Model.Id})%>
        |
        <%:Html.ActionLink(Localization.GetMessage("BackToList"), "Index")%>
    </p>
    <table id="groupUsersTable">
        <thead>
            <tr>
                <th>
                    <%=Localization.GetMessage("Username")%>
                </th>
                <th>
                    <%=Localization.GetMessage("Name")%>
                </th>
                <th class="actionsColumn">
                </th>
            </tr>
        </thead>
        <tbody>
            <%
                if (Model.GroupUsers.GetEnumerator().MoveNext())
                {
                    foreach (var groupUser in Model.GroupUsers)
                    {%>
            <tr>
                <td>
                    <%:groupUser.User.Username%>
                </td>
                <td>
                    <%:groupUser.User.Name%>
                </td>
                <td>
                    <%:Html.ActionLink(Localization.GetMessage("RemoveUser"), "RemoveUser",
                                                              new {id = Model.Id, userRef = groupUser.User.Id})%>
                </td>
            </tr>
            <%
                        } %>
        </tbody>
    </table>
    <%
                    }
                    else
                    {%>
    <%=Localization.GetMessage("NoData")%>
    <%}%>
</asp:Content>
