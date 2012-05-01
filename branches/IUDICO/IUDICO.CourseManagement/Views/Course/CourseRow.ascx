<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CourseManagement.Models.ViewCourseModel>" %>
<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="IUDICO.CourseManagement.Models" %>
<%@ Import Namespace="IUDICO.Common" %>
<%@ Import Namespace="System.Web.Mvc" %>


<tr id="course<%: Model.Id %>" class="<%: Model.Locked? "courseLocked" : Model.Shared ? "courseShared" : "courseMy"%> course">
    <td class="courseCheck">
        <input type="checkbox" id="<%= Model.Id %>" />
    </td>
    <td class="<%: Model.Locked ? "" : "courseEditable" %>">
        <div class="courseName"><%: Model.Name%></div>
        <div class="courseCreatedBy"><%:  Localization.GetMessage("CreatedBy") %> <%: (Model.Shared ? Model.OwnerName : Localization.GetMessage("me")) %>: <%: DateFormatConverter.DataConvert(Model.Created) %></div>
    </td>
    <td>
        <div>
            <span class="updatedOnField">
            <%: DateFormatConverter.DataConvert(Model.Updated) %>
            </span>
            <span class="updatedByField">
            <%: Model.UpdatedByName %>
            </span>
        </div>
    </td>
    <td>
<<<<<<< .mine
        <a href="#" onclick="editCourse(<%: Model.Id %>)"><%=Localization.GetMessage("Edit")%></a>
        |
=======
        <a href="#" onclick="editCourse(<%: Model.Id %>)" class="buttonEdit button" title="<%: Localization.GetMessage("Rename") %>"></a>
>>>>>>> .r1972
        <% if(Model.Locked) { %>
            <%: MvcHtmlString.Create(Html.ActionLink("[temp]", "Parse", "Course", new { CourseID = Model.Id }, new { @class = "buttonUnlock button", title = Localization.GetMessage("Unlock") }).ToString().Replace("[temp]", ""))%>
        <% }
           else
           { %>
            <%: MvcHtmlString.Create(Html.ActionLink("[temp]", "Publish", new { CourseID = Model.Id }, new { @class = "buttonLock button", title = Localization.GetMessage("Lock") }).ToString().Replace("[temp]", "")) %>
        <% }  %>
<<<<<<< .mine
        |
        <%: Html.ActionLink(Localization.GetMessage("Export"), "Export", new { CourseID = Model.Id })%>
        |
        <%: Ajax.ActionLink(Localization.GetMessage("Delete"), "Delete", new { CourseID = Model.Id }, new AjaxOptions { Confirm = Localization.GetMessage("AreYouSureYouWantToDelete") + Model.Name + "\"?", HttpMethod = "Delete", OnSuccess = "removeRow" })%>
        |
        <a href="#" onclick="shareCourse(<%: Model.Id %>)"><%=Localization.GetMessage("Share") %></a>
=======
        <a href="<%: Url.Action("Export", new { CourseID = Model.Id })%>" title="<%: Localization.GetMessage("Export") %>" class="buttonExport button"></a>
        
        
        <%: MvcHtmlString.Create(Ajax.ActionLink("[temp]", "Delete", new { CourseID = Model.Id }, new AjaxOptions { Confirm = "Are you sure you want to delete \"" + Model.Name + "\"?", HttpMethod = "Delete", OnSuccess = "removeRow" }, new { @class = "buttonDelete button", title = Localization.GetMessage("Delete") }).ToString().Replace("[temp]", ""))%>
        <a href="#" onclick="shareCourse(<%: Model.Id %>)" class="buttonShare button" title="<%: Localization.GetMessage("Share") %>"></a>
>>>>>>> .r1972
    </td>
</tr>