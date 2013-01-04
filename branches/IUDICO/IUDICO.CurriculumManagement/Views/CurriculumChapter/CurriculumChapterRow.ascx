<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewCurriculumChapterModel>" %>
<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

		<tr id="curriculumChapter<%= Model.Id %>" class="child-of-curriculum<%= Model.CurriculumRef %>">
          <td></td>
			 <td></td>
			 <td id="chapterIndex" style="text-align: right;" class="curriculum-important-field">
				 <div class="collapsedIcon" style="float: left;"></div>
			 </td>
          <td id="chapterName" class="clickable curriculum-chapter-name" onclick="onRowClick(<%: Model.Id %>);">
              <%: Model.ChapterName %>
          </td>
          <td class="clickable" onclick="onRowClick(<%: Model.Id %>);">
              <%: String.Format("{0:g}", Model.StartDate)%>
          </td>
          <td class="clickable" onclick="onRowClick(<%: Model.Id %>);">
              <%: String.Format("{0:g}", Model.EndDate)%>
          </td>
          <td>
	          <a href="javascript:void(0)" onclick="CurriculumChapterEditClick(<%= Model.Id %>); event.stopPropagation();" ><%= Localization.GetMessage("Edit") %></a>
				 <%if(Model.HaveTimelines){ %><span id="removeTimelinesLink">|
				 <a href="javascript:void(0)" onclick="RemoveCurriculumChapterTimelines(<%= Model.Id %>); event.stopPropagation();" ><%= Localization.GetMessage("RemoveTimelines") %></a></span><%} %>
          </td>
      </tr>