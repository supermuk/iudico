<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewCurriculumChapterTopicModel>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<tr id="curriculumChapterTopic<%= Model.Id %>" class="child-of-curriculumChapter<%= Model.CurriculumChapterRef %>" <% if (Model.BlockCurriculumAtTesting || Model.BlockTopicAtTesting) {%>style="background-color: #F97878;"<%} %>>
          <td></td>
			 <td></td>
			 <td id="chapterTopicIndex" style="text-align: right;" class="curriculum-important-field">
			 </td>
          <td id="topicName" class="clickable curriculum-chapter-name" onclick="onRowClick(<%: Model.Id %>);">
              <%: Model.TopicName %>
				  <br/>
				  <%=Localization.GetMessage("ThresholdOfSuccess")%>: <%: Model.ThresholdOfSuccess %>
          </td>
          <td class="clickable" onclick="onRowClick(<%: Model.Id %>);">
              <%: String.Format("{0:g}", Model.TestStartDate)%>
				  <br/>
				  <%: String.Format("{0:g}", Model.TheoryStartDate)%>
          </td>
          <td class="clickable" onclick="onRowClick(<%: Model.Id %>);">
              <%: String.Format("{0:g}", Model.TestEndDate)%>
				  <br/>
				  <%: String.Format("{0:g}", Model.TheoryEndDate)%>
          </td>
          <td>
	          <a href="javascript:void(0)" onclick="CurriculumChapterTopicEditClick(<%= Model.Id %>)" ><%= Localization.GetMessage("Edit") %></a>
          </td>
      </tr>

