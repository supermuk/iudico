<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Feature>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Features
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Features</h2>

     <table>
        <tr>
            <th>Name</th>
            <th></th>
        </tr>

     <%
         foreach (var item in Model)
         {%>
        <tr>
            <td>
            <%:item.Name%>
            </td>
            <td>
                <%:Html.ActionLink("Edit Topics", "EditTopics", new { id = item.Id })%> |
                <%:Html.ActionLink("Edit", "Edit", new { id = item.Id })%> |
                <%:Ajax.ActionLink("Delete", "Delete", new { id = item.Id },
                                               new AjaxOptions
                                                   {
                                                       Confirm = "Are you sure you want to delete \"" + item.Name + "\"?",
                                                       HttpMethod = "Delete",
                                                       OnSuccess = "removeRow"
                                                   })%>
            </td>
        </tr>
     <%
         }%>

     </table>

     <p>
        <%:Html.ActionLink("Create new feature", "Create")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Html.ResolveUrl("/Scripts/Microsoft/MicrosoftAjax.js")%>" type="text/javascript"></script>
    <script src="<%=Html.ResolveUrl("/Scripts/Microsoft/MicrosoftMvcAjax.js")%>" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function removeRow(data) {
            window.location = window.location;
        }
    </script>
</asp:Content>