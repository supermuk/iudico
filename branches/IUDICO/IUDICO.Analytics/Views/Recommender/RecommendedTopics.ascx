<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<IUDICO.Common.Models.Shared.CurriculumManagement.TopicDescription>>" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared.DisciplineManagement" %>

<div class="recommended">
<% foreach (var package in Model) {%>
    <li class="topicLink topicName">
                         
    <%: Html.ActionLink("[" + package.TopicType.ToString() + "] " + package.Topic.Name + " ",
                "Play",
                "Training",
                new {
                    curriculumChapterTopicId = package.CurriculumChapterTopicId,
                    courseId = package.CourseId, 
                    topicType = package.TopicType 
                    },
                new {@class = package.TopicType == TopicTypeEnum.Test ? "test" : "theory", @title = "Start " + package.Discipline.Name + "/" + package.Chapter.Name + "/" + package.Topic.Name}) %>
                         
    </li>
<% } %>
</div>