<%@ Assembly Name="IUDICO.UserManagement" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Group>>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $('#groupsTable').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                iDisplayLength: 50,
                "bSort": true,
                "aoColumns": [
                    null,
                    { "bSortable": false }
                ]
            });

        });

        function detailsClick(id) {
            window.location.replace("/Group/Details?id=" + id);
        }

        function removeRow(result) {
            if (result) {
                document.location = document.location;
            }
            else {
                alert("Can delete selected group. It's active or doesn't exist.");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("Groups")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Localization.GetMessage("Groups")%></h2>
    <p>
        <%:Html.ActionLink(Localization.GetMessage("CreateNewGroup"), "Create")%>
    </p>
    <%
        if (Model.GetEnumerator().MoveNext())
        { %>
    <table id="groupsTable">
        <thead>
            <tr>
                <th>
                    <%=Localization.GetMessage("Name1")%>
                </th>
                <th>
                </th>
            </tr>
        </thead>
        <tbody>
            <%
            foreach (var item in Model)
            {%>
            <tr>
                <td class="clickable" onclick="detailsClick(<%:item.Id%>);">
                    <%:item.Name%>
                </td>
                <td>
                    <%:Html.ActionLink(Localization.GetMessage("Edit"), "Edit", new {id = item.Id})%>
                    |
                    <%:Ajax.ActionLink(Localization.GetMessage("Delete"), "Delete", new {id = item.Id},
                                                  new AjaxOptions
                                                      {
                                                          Confirm =
                                                              "Are you sure you want to delete \"" + item.Name + "\"?",
                                                          HttpMethod = "Delete",
                                                          OnSuccess = "removeRow"
                                                      })%>
                </td>
            </tr>
            <%
            } %>
        </tbody>
    </table>
    <%}
        else
        {%>
    <%=Localization.GetMessage("NoData")%>
    <%
        }%>
</asp:Content>
