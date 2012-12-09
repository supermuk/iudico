<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Tag>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Tags
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Tags</h2>

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
                                                       OnSuccess = "onSuccess"
                                                   })%>
                 
            </td>
        </tr>
     <%
         }%>

     </table>

     <p>
        <%:Html.ActionLink("Create new tag", "Create")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Html.ResolveUrl("/Scripts/Microsoft/MicrosoftAjax.js")%>" type="text/javascript"></script>
    <script src="<%=Html.ResolveUrl("/Scripts/Microsoft/MicrosoftMvcAjax.js")%>" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function onSuccess(result) {
            var data = result.get_object();
            if (data.Message == true) {
                window.location = window.location;
            } else {
                alert("Can't delete tag, which has topics assigned to it");
            }
        }
    </script>
</asp:Content>