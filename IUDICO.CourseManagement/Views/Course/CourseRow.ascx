<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CourseManagement.Models.ViewCourseModel>" %>
<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="IUDICO.CourseManagement.Models" %>


<tr id="course<%: Model.Id %>" class="<%: Model.Locked? "courseLocked" : Model.Shared ? "courseShared" : "courseMy"%>" course">
    <td class="courseCheck">
        <input type="checkbox" id="<%= Model.Id %>" />
    </td>
    <td class="<%: Model.Locked ? "" : "courseEditable" %>">
        <div><%: Model.Name%></div>
    </td>
    <td>
        <div class="course-created-label">
            <%: Model.Shared ? Model.OwnerName : IUDICO.CourseManagement.Localization.GetMessage("me") %>
        </div>
    </td>
    <td>
        <div>
            <%: DateFormatConverter.DataConvert(Model.Updated) %>
        </div>
    </td>
    <td>
        <a href="#" onclick="editCourse(<%: Model.Id %>)">Edit</a>
        |
        <% if(Model.Locked) { %>
            <%: Html.ActionLink(IUDICO.CourseManagement.Localization.GetMessage("Unlock"), "Parse", "Course", new { CourseID = Model.Id }, null)%>
        <% }
           else
           { %>
            <%: Html.ActionLink(IUDICO.CourseManagement.Localization.GetMessage("Lock"), "Publish", new { CourseID = Model.Id })%>
        <% }  %>
        |
        <%: Html.ActionLink(IUDICO.CourseManagement.Localization.GetMessage("Export"), "Export", new { CourseID = Model.Id })%>
        |
        <%: Ajax.ActionLink(IUDICO.CourseManagement.Localization.GetMessage("Delete"), "Delete", new { CourseID = Model.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + Model.Name + "\"?", HttpMethod = "Delete", OnSuccess = "removeRow" })%>
        |
        <a href="#" onclick="shareCourse(<%: Model.Id %>)">Share</a>
    </td>
</tr>