<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Course>>" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%= Html.ResolveUrl("/Scripts/Microsoft/MicrosoftAjax.js")%>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("/Scripts/Microsoft/MicrosoftMvcAjax.js")%>" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("<%=IUDICO.CourseManagement.Localization.getMessage("PleaseSelectCoursesDelete") %>");
                    
                    return false;
                }

                var answer = confirm("<%=IUDICO.CourseManagement.Localization.getMessage("AreYouSureYouWantDelete") %>" + ids.length + "<%=IUDICO.CourseManagement.Localization.getMessage("selectedCourses") %>");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/Course/Delete",
                    data: { courseIds: ids },
                    success: function (r) {
                        if (r.success) {
                            $("td input:checked").parents("tr").remove();
                        }
                        else {
                            alert("<%=IUDICO.CourseManagement.Localization.getMessage("ErrorOccuredDuringProccessingRequest") %>");
                        }
                    }
                });
            });

        });
        function removeRow(data) {
            window.location = window.location;
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <p>
        
        <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("CreateNew"), "Create", "Course")%> |
        <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Import"), "Import")%> |
        <a id="DeleteMany" href="#"><%=IUDICO.CourseManagement.Localization.getMessage("DeleteSelected")%></a>
    </p>
    <h2><%=IUDICO.CourseManagement.Localization.getMessage("Mycourses")%>:</h2>
    <% var index = 1; %>
    <% if (Model.Where(i => i.Owner == HttpContext.Current.User.Identity.Name).Count() > 0)
       { %>
    <table>
        <tr>
            <th></th>
            <th>
                №
            </th>
            <th>
                <%=IUDICO.CourseManagement.Localization.getMessage("Name")%>
            </th>
            <th>
                <%=IUDICO.CourseManagement.Localization.getMessage("Created")%>
            </th>
            <th>
                <%=IUDICO.CourseManagement.Localization.getMessage("Updated")%>
            </th>
            <th></th>
        </tr>

    <% foreach (var item in Model.Where(i => i.Owner == HttpContext.Current.User.Identity.Name))
       { %>
    
        <tr>
            <td>
                <input type="checkbox" id="<%= item.Id %>" />
            </td>
            <td>
                <%: index++%>
            </td>
            <td>
                <%: item.Name%>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Created)%>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Updated)%>
            </td>
            <td>
                <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("EditCourse"), "Edit", "Course", new { CourseID = item.Id }, null)%> |
                <% if (item.Locked == null || item.Locked.Value == false)
                   { %>
                <%:Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("EditContentCourse"), "Index", "Node", new { CourseID = item.Id }, null)%> |
                <% }
                   else
                   {%>
                <%:Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Unlock"), "Parse", "Course", new { CourseID = item.Id }, null)%> |
                <%}%>
                <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Export"), "Export", new { CourseID = item.Id })%> |
                <%: Ajax.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Delete"), "Delete", new { CourseID = item.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + item.Name + "\"?", HttpMethod = "Delete", OnSuccess = "removeRow" })%>
            </td>
        </tr>
    
    <% } %>
    </table>
    <% } else {%>
         <%=IUDICO.CourseManagement.Localization.getMessage("NoCourses")%>
    <% } %>

    <h2><%=IUDICO.CourseManagement.Localization.getMessage("CoursesSharedWithMe")%>:</h2>
    <% if (Model.Where(i => i.Owner != HttpContext.Current.User.Identity.Name).Count() > 0)
       { %>
    <table>
        <tr>
            <th></th>
            <th>
                №
            </th>
            <th>
                <%=IUDICO.CourseManagement.Localization.getMessage("Name")%>
            </th>
            <th>
                <%=IUDICO.CourseManagement.Localization.getMessage("Owner")%>
            </th>
            <th>
                <%=IUDICO.CourseManagement.Localization.getMessage("Created")%>
            </th>
            <th>
                <%=IUDICO.CourseManagement.Localization.getMessage("Updated")%>
            </th>
            <th></th>
        </tr>

    <% foreach (var item in Model.Where(i => i.Owner != HttpContext.Current.User.Identity.Name)) { %>
    
        <tr>
            <td>
                <input type="checkbox" id="<%= item.Id %>" />
            </td>
            <td>
                <%: index++ %>
            </td>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.Owner %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Created) %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Updated) %>
            </td>
            <td>
                <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Edit"), "Edit", "Course", new { CourseID = item.Id }, null)%> |
                <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Details"), "Index", "Node", new { CourseID = item.Id }, null)%> |
                <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Export"), "Export", new { CourseID = item.Id })%> |
                <%: Ajax.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Delete"), "Delete", new { CourseID = item.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + item.Name + "\"?", HttpMethod = "Delete", OnSuccess = "removeRow" })%>
            </td>
        </tr>
    
    <% } %>

    </table>
    <% } else {%>
         <%=IUDICO.CourseManagement.Localization.getMessage("NoCourses")%>
    <% } %>


</asp:Content>

