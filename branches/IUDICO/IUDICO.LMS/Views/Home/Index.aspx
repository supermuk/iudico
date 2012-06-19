<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.LMS.Models.HomeModel>" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared.DisciplineManagement" %>
<%@ Import Namespace="IUDICO.Common" %>
<%@ Import Namespace="IUDICO.Common.Models.Services" %>
<%@ Import Namespace="IUDICO.LMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	IUDICO
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    $(function () {
        $('.rating').rating({
            callback: function (value, link) {
                $(this).rating('readOnly');

                var rest = $(this).attr('name').split('_');
                var topicId = rest[1];

                $.post('/Account/RateTopic', { 'topicId': topicId, 'score': value }, function (data) {

                });
            }
        });
    });
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%=Localization.GetMessage("WelcomeIudico")%></h1>

    <% var user = MvcApplication.StaticContainer.GetService<IUserService>().GetCurrentUser(); %>

    <% if(user != null && user.Roles != null && user.Roles.Contains(Role.Teacher)) {%>
    <div>
        <div class="homeArea">
        <div class="homeAreaTitle">
            <%: Localization.GetMessage("CourseManagement") %>
        </div>
        <div class="homeActionList">
        <ul>
        <% foreach (var action in Model.Actions.Single(i => i.Key.GetName() == "CourseManagement").Value)
           { %>
            <li>
                <a href="<%: action.Link %>"><%: action.Name %></a>
            </li>
        <% } %>
        </ul>
        </div>
        <div class="homeActionLogo">
        <img src="/Content/Images/course_logo.png" class="homeActionImg" alt="<%: Localization.GetMessage("CourseManagement") %>"/>
        </div>
    </div>
        <div class="verticalLine"></div>
        <div class="homeArea">
        <div class="homeAreaTitle">
            <%: Localization.GetMessage("CurriculumManagement") %>
        </div>
        <div class="homeActionList">
        <ul>
        <% foreach (var action in Model.Actions.Single(i => i.Key.GetName() == "CurriculumManagement").Value)
           { %>
            <li>
                <a href="<%: action.Link %>"><%: action.Name %></a>
            </li>
        <% } %>
        <% foreach (var action in Model.Actions.ElementAt(3).Value)
           { %>
            <li>
                <a href="<%: action.Link %>"><%: action.Name %></a>
            </li>
        <% } %>
        </ul>
        </div>
        <div class="homeActionLogo">
            <img src="/Content/Images/curriculum_logo.png" class="homeActionImg" alt="<%: Localization.GetMessage("CurriculumManagement") %>"/>
        </div>
    </div>
        <div class="verticalLine"></div>
        <div class="homeArea">
        <div class="homeAreaTitle">
            <%: Localization.GetMessage("Statistic") %>
        </div>
        <div class="homeActionList">
        <ul>
        <% foreach (var action in Model.Actions.Single(i => i.Key.GetName() == "Statistics").Value)
           { %>
            <li>
                <a href="<%: action.Link %>"><%: action.Name %></a>
            </li>
        <% } %>
        </ul>
        </div>
        <div class="homeActionLogo">
            <img src="/Content/Images/statistic_logo.png" class="homeActionImg" alt="<%: Localization.GetMessage("Statistic") %>"/>
        </div>
    </div>
        <div class="verticalLine"></div>
        <div class="homeArea">
        <div class="homeAreaTitle">
            <%: Localization.GetMessage("UserManagment") %>
        </div>
        <div class="homeActionList">
        <ul>
        <% foreach (var action in Model.Actions.Single(i => i.Key.GetName() == "UserManagement").Value)
           { %>
           <% if (action.Link == "User/Index" || action.Link == "Group/Index") %>
           <% { %>
            <li>
                <a href="<%: action.Link %>"><%: action.Name%></a>
            </li>
            <% } %>
        <% } %>
        </ul>
        </div>
        <div class="homeActionLogo">
            <img src="/Content/Images/users_logo.png" class="homeActionImg" alt="<%: Localization.GetMessage("UserManagment") %>"/>
        </div>
    </div>
        <div style="clear:both;"></div>
    </div>
    <% } %>

	<% if (Model.TopicsDescriptions.Any())
	   {%>
    <div>
    <h2 class="availableTopics"><%= Localization.GetMessage("AvailableTopics") %></h2>
        
    <div>
    <% foreach (var dis in Model.GroupedTopicsDescriptions)
	   {%>
           <div class="homeDiscipline">
               <div class="homeDisciplineName">
                <%: dis.Key %>
               </div>

               <div class="homeChapters">
               <% foreach (var chapter in dis.Value)
				  {%>
                    <div class="homeChapter">
                        <div class="homeChapterName">
                            <%: chapter.Key %>
                        </div>

                        <div class="homePackages">
                        <% foreach (var package in chapter.Value)
					       {%>
                             <div class="homePackage">
                             <div class="homePackageName">
                                 <span class="homeTopicType">[<%: Localization.GetMessage(package.TopicType.ToString()) %>] </span>
                             <%: Html.ActionLink( package.Topic.Name + " ",
                                            "Play",
                                            "Training",
                                            new {
                                                curriculumChapterTopicId = package.CurriculumChapterTopicId,
                                                courseId = package.CourseId, 
                                                topicType = package.TopicType 
                                                },
                                            new {@class = package.TopicType == TopicTypeEnum.Test ? "test" : "theory", @title = "Start " + package.Discipline.Name + "/" + package.Chapter.Name + "/" + package.Topic.Name}) %>
                                </div>
                                <div class="raitingHolder">
                                <% for (var i = 1; i <= 5; ++i)
                                    { %>
                                    <input name="rating_<%= package.Topic.Id + "_" + package.CurriculumChapterTopicId + "_" + package.CourseId + "_" + package.TopicType.ToString()%>" value="<%=i%>" <%= (Model.TopicsRatings.ContainsKey(package.Topic.Id) && Model.TopicsRatings[package.Topic.Id] == i ? "checked='checked'" : "") %> <%= (Model.TopicsRatings.ContainsKey(package.Topic.Id) ? "disabled='disabled'" : "") %> type="radio" class="rating required"/>
                                <% } %>
                                </div>
                            </div>
                        <% } %>
                        </div>
                   </div>
               <% } %>
               </div>
           </div>
       
  
       <% } %>
       </div>
    </div>
	<% } %>
	
    <%: Html.Action("RecommendedTopics", "Recommender")%>
</asp:Content>
