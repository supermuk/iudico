<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<IUDICO.Common.Models.Shared.TopicStat>>" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared.DisciplineManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<div class="recommended">
    <h2><%: Localization.GetMessage("Recommended") %></h2>
<% foreach (var ts in Model) {%>
    <li class="topicLink topicName">
                         
    <%: Html.ActionLink("[" + ts.Topic.TopicType.ToString() + "] " + ts.Topic.Topic.Name + " ",
                "Play",
                "Training",
                new {
                    curriculumChapterTopicId = ts.Topic.CurriculumChapterTopicId,
                    courseId = ts.Topic.CourseId,
                    topicType = ts.Topic.TopicType 
                    },
                new { @class = ts.Topic.TopicType == TopicTypeEnum.Test ? "test" : "theory", @title = "Start " + ts.Topic.Discipline.Name + "/" + ts.Topic.Chapter.Name + "/" + ts.Topic.Topic.Name })%>
                
        [<%: float.IsNaN(ts.Score) ? 'N/A' : ts.Score + "%" %>]
    </li>
<% } %>
</div>