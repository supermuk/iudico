﻿<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Course>>" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%= Html.ResolveUrl("/Scripts/Microsoft/MicrosoftAjax.js")%>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("/Scripts/Microsoft/MicrosoftMvcAjax.js")%>" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("input[id$='CheckAll']").click(function () {
                var $boxes = $("#" + this.id.replace("CheckAll", "")).find(":checkbox");
                if(this.checked) {
                    $boxes.attr("checked", "checked");
                } else {
                    $boxes.removeAttr("checked");
                }
            });

            $("#DeleteMany").click(function () {
                var ids = $("td input:checked:not(id$='CheckAll')").map(function () {
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
    <%=IUDICO.CourseManagement.Localization.getMessage("Courses")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("CreateNew"), "Create", "Course")%>
        |
        <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Import"), "Import")%>
        | <a id="DeleteMany" href="#">
            <%=IUDICO.CourseManagement.Localization.getMessage("DeleteSelected")%></a>
    </p>
    <div style="float: left; width:600px;">
        <h2>
            <%=IUDICO.CourseManagement.Localization.getMessage("Mycourses")%>:</h2>
        <% var index = 1;
           var myCourses = Model.Where(i => i.Owner == HttpContext.Current.User.Identity.Name && i.Locked != true);
           var sharedCourses = Model.Where(i => i.Owner != HttpContext.Current.User.Identity.Name && i.Locked != true);
           var lockedCourses = Model.Where(i => i.Locked == true);
        %>
        <% if (myCourses.Count() > 0)
           { %>
        <table id="myCourses">
            <tr>
                <th>
                    <input type="checkbox" id="myCoursesCheckAll" />
                </th>
                <th>
                    №
                </th>
                <th>
                    <%=IUDICO.CourseManagement.Localization.getMessage("Name")%>
                </th>
                <th>
                    <%=IUDICO.CourseManagement.Localization.getMessage("Updated")%>
                </th>
                <th>
                </th>
            </tr>
            <% foreach (var item in myCourses)
               { %>
            <tr>
                <td>
                    <input type="checkbox" id="<%= item.Id %>" />
                </td>
                <td>
                    <%: index++%>
                </td>
                <td>
                    <div><%: item.Name%></div>
                    <div class="course-created-label"><%=IUDICO.CourseManagement.Localization.getMessage("Created by me")%>: <%: String.Format("{0:d}", item.Created)%></div>
                </td>
                <td>
                    <%: String.Format("{0:g}", item.Updated)%>
                </td>
                <td>
                    <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("EditCourse"), "Edit", "Course", new { CourseID = item.Id }, null)%>
                    |
                    <%:Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("EditContentCourse"), "Index", "Node", new { CourseID = item.Id }, null)%>
                    |
                    <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Publish"), "Publish", new { CourseID = item.Id })%>
                    |
                    <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Export"), "Export", new { CourseID = item.Id })%>
                    |
                    <%: Ajax.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Delete"), "Delete", new { CourseID = item.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + item.Name + "\"?", HttpMethod = "Delete", OnSuccess = "removeRow" })%>
                </td>
            </tr>
            <% } %>
        </table>
        <% }
           else
           {%>
        <%=IUDICO.CourseManagement.Localization.getMessage("NoCourses")%>
        <% } %>
        <h2>
            <%=IUDICO.CourseManagement.Localization.getMessage("CoursesSharedWithMe")%>:</h2>
        <% if (sharedCourses.Count() > 0)
           { %>
        <table id="sharedCourses">
            <tr>
                <th>
                    <input type="checkbox" id="sharedCoursesCheckAll" />
                </th>
                <th>
                    №
                </th>
                <th>
                    <%=IUDICO.CourseManagement.Localization.getMessage("Name")%>
                </th>
                <th>
                    <%=IUDICO.CourseManagement.Localization.getMessage("Updated")%>
                </th>
                <th>
                </th>
            </tr>
            <% foreach (var item in sharedCourses)
               { %>
            <tr>
                <td>
                    <input type="checkbox" id="<%= item.Id %>" />
                </td>
                <td>
                    <%: index++ %>
                </td>
                <td>
                    <div><%: item.Name %></div>
                    <div class="course-created-label"><%=IUDICO.CourseManagement.Localization.getMessage("Created by")%> <%: item.Owner %>: <%: String.Format("{0:d}", item.Created) %>)</div>
                </td>
                <td>
                    <%: String.Format("{0:g}", item.Updated) %>
                </td>
                <td>
                    <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Edit"), "Edit", "Course", new { CourseID = item.Id }, null)%>
                    |
                    <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Details"), "Index", "Node", new { CourseID = item.Id }, null)%>
                    |
                    <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Publish"), "Publish", new { CourseID = item.Id })%>
                    |
                    <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Export"), "Export", new { CourseID = item.Id })%>
                    |
                    <%: Ajax.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Delete"), "Delete", new { CourseID = item.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + item.Name + "\"?", HttpMethod = "Delete", OnSuccess = "removeRow" })%>
                </td>
            </tr>
            <% } %>
        </table>
        <% }
           else
           {%>
        <%=IUDICO.CourseManagement.Localization.getMessage("NoCourses")%>
        <% } %>
    </div>
    <div style="float: left; width: 300px;  margin-left:20px;">
        <h2>
            <%= IUDICO.CourseManagement.Localization.getMessage("Published courses") %>:</h2>
        <% if (lockedCourses.Count() > 0)
           { %>
        <table id="publishedCourses">
            <tr>
                <th>
                    <input type="checkbox" id="publishedCoursesCheckAll" />
                </th>
                <th>
                    №
                </th>
                <th>
                    <%=IUDICO.CourseManagement.Localization.getMessage("Name")%>
                </th>
                <th>
                    <%=IUDICO.CourseManagement.Localization.getMessage("Published")%>
                </th>
                <th>
                </th>
            </tr>
            <% foreach (var item in lockedCourses)
               { %>
            <tr>
                <td>
                    <input type="checkbox" id="<%= item.Id %>" />
                </td>
                <td>
                    <%: index++ %>
                </td>
                <td>
                    <div><%: item.Name %></div>
                    <div class="course-created-label"><%=IUDICO.CourseManagement.Localization.getMessage("Created by")%> <%: item.Owner %></div>
                </td>
                <td>
                    <%: String.Format("{0:g}", item.Updated) %>
                </td>
                <td>
                    <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Edit"), "Edit", "Course", new { CourseID = item.Id }, null)%>
                    |
                    <%:Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Unlock"), "Parse", "Course", new { CourseID = item.Id }, null)%>
                    |
                    <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Download"), "Export", new { CourseID = item.Id })%>
                    |
                    <%: Ajax.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Delete"), "Delete", new { CourseID = item.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + item.Name + "\"?", HttpMethod = "Delete", OnSuccess = "removeRow" })%>
                </td>
            </tr>
            <% } %>
        </table>
        <% }
           else
           {%>
        <%=IUDICO.CourseManagement.Localization.getMessage("NoCourses")%>
        <% } %>
    </div>
    <div style="clear:both;"></div>
</asp:Content>