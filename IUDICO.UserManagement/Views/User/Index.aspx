<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.User>>" %>
<%@ Import Namespace="IUDICO.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.getMessage("Users")%>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Html.ResolveUrl("~/Scripts/jquery-ui.js")%>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery.dataTables.editable.js")%>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery.dataTables.min.js")%>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery.jeditable.js")%>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery.validate.js")%>" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $('#myDataTable').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                iDisplayLength: 50,
                "bSort": true,
                "aoColumns": [
                null,
                null,
                { "bSortable": false },
                null,
                null,
                null,
                { "bSortable": false }
                ]
            });
        });
        
    </script>


    <h2><%=Localization.getMessage("Users")%></h2>

    <div id="demo">

    <table id="myDataTable" class="display">

    <thead>

        <tr>
            <th>
                <%=Localization.getMessage("FullName")%>
            </th>
            <th>
                <%=Localization.getMessage("Loginn")%>
            </th>
            <th>
                <%=Localization.getMessage("Active")%>
            </th>
            <th>
                <%=Localization.getMessage("ApprovedBy")%>
            </th>
            <th>
                <%=Localization.getMessage("CreationDate")%>
            </th>
            <th>
                <%=Localization.getMessage("Groups")%>
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
            <td>
                <%:item.Name%>
            </td>
            <td>
                <%:item.Username%>
            </td>
            <td>
                <%:item.IsApproved.ToString()%>
            </td>
            <td>
                <%:item.User1 != null ? item.User1.Username : string.Empty%>
            </td>
            <td>
                <%:item.CreationDate.ToString()%>
            </td>
            <td>
                <%:item.GroupsLine%>
            </td>
            <td>
                <%
            if (item.IsApproved)
            {%>
                    <%:Html.ActionLink(Localization.getMessage("Deactivate"), "Deactivate",
                                                  new {id = item.Id})%> |
                <%
            }
            else
            {%>
                    <%:Html.ActionLink(Localization.getMessage("Activate"), "Activate", new {id = item.Id})%> |
                <%
            }%>
                <%:Html.ActionLink(Localization.getMessage("Edit"), "Edit", new {id = item.Id})%> |
                <%:Html.ActionLink(Localization.getMessage("Details"), "Details", new {id = item.Id})%> |
                <%:Html.ActionLink(Localization.getMessage("AddToRole"), "AddToRole", new {id = item.Id})%> |
                <%:Html.ActionLink(Localization.getMessage("AddToGroup"), "AddToGroup", new {id = item.Id})%> |
                <%:Ajax.ActionLink(Localization.getMessage("Delete"), "Delete", new {id = item.Id},
                                              new AjaxOptions
                                                  {
                                                      Confirm =
                                                          "Are you sure you want to delete \"" + item.Username + "\"?",
                                                      HttpMethod = "Delete",
                                                      OnSuccess = "removeRow"
                                                  })%>
            </td>
        </tr>
    
    <%
        }%>

        </tbody>

    </table>

    </div>

    <p>
        <%:Html.ActionLink(Localization.getMessage("CreateNewUser"), "Create")%> | 
        <%:Html.ActionLink(Localization.getMessage("CreateNewUsers"), "CreateMultiple")%>
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


    <link href="<%=  Html.ResolveUrl("~/Content/dataTables/demo_table.css")     %>" rel="stylesheet" type="text/css" />
    <link href="<%=  Html.ResolveUrl("~/Content/dataTables/demo_table_jui.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=  Html.ResolveUrl("~/Content/themes/base/jquery-ui.css")     %>" rel="stylesheet" type="text/css" media="all" />
    <link href="<%=  Html.ResolveUrl("~/Content/jAlert/jquery.alerts.css")      %>" rel="stylesheet" type="text/css" />
    <script src="<%= Html.ResolveUrl("~/Content/jAlert/jquery.alerts.js")       %>" type="text/javascript"></script>



</asp:Content>

