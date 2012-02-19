<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Group>>" %>
<%@ Import Namespace="IUDICO.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.getMessage("Groups")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.getMessage("Groups")%></h2>

    <table>
        <tr>
            <th>
                <%=Localization.getMessage("Name1")%>
            </th>
            <th></th>
        </tr>

    <%
        if (Model.GetEnumerator().MoveNext())
        {
            foreach (var item in Model)
            {%>
    
        <tr>
            <td>
                <%:item.Name%>
            </td>
            <td>
                <%:Html.ActionLink(Localization.getMessage("Edit"), "Edit", new {id = item.Id})%> |
                <%:Html.ActionLink(Localization.getMessage("Details"), "Details", new {id = item.Id})%> |
                <%:Ajax.ActionLink(Localization.getMessage("Delete"), "Delete", new {id = item.Id},
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
            }
        }
        else
        {%>
        <tr>
            <td>
                <%=Localization.getMessage("NoData")%>
            </td>
            <td>
                <%=Localization.getMessage("NoActions")%>
            </td>
        </tr>
    <%
        }%>
    </table>

    <p>
        <%:Html.ActionLink(Localization.getMessage("CreateNewGroup"), "Create")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script>
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

