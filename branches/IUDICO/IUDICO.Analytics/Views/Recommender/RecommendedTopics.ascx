<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<IUDICO.Common.Models.Shared.CurriculumManagement.TopicDescription>>" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared.DisciplineManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<div class="recommended">
    <h2><%: Localization.GetMessage("Recommended") %></h2>
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