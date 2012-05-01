<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.LMS.Models.HomeModel>" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared.DisciplineManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

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

<style type="text/css">
    .topicLink:hover 
    {
    	background-color: whitesmoke;
		border-width: 2px;
    	border-style: solid;
    	border-color: gainsboro; 
		border-radius: 7px;
    }
    .topicLink 
    {
    	display: table;
    }
    .availableTopics 
    {
    	color: slategrey;
    	font-size: 20px;
		text-shadow: 1px 1px 2px #b9bec9;
		filter: dropshadow(color=#b9bec9, offx=1, offy=1);
    }
    .disciplineName 
    {
    	font-size: 16px;
    	font-weight: bold;
    }
    .chapterName 
    {
    	font-size: 14px;
    	font-style: oblique;
    	font-weight: normal;
    }
    .topicName 
    {
    	font-size: 13px;
    	font-style: normal;
    	text-decoration: underline;
    	
    }
	A.test
	{
		color: orange;
	}
	A.theory
	{
		color: green;
	}
	.ul 
	{
		padding: 0;
		margin-left: 20px;
		list-style-type: none;
	}
</style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3><%=Localization.GetMessage("WelcomeIudico")%></h3>
    
    <% foreach (var plugin in Model.Actions) { %>
        <% if (plugin.Value.Any())
            {%>
        <h4><%= plugin.Key.GetName() %></h4>
        <ul>
        <% 
               foreach (var item in plugin.Value)
            { %>
            <li><a href="<%= item.Link %>"><%= item.Name %></a></li>
        <% } %>
        </ul>
        <% } %>
    <% } %>    
    
	<% if (Model.TopicsDescriptions.Any())
	   {%>
    <h4 class="availableTopics"><%= Localization.GetMessage("AvailableTopics") %></h4>
    <div>
    <ul class="topics">
    <% foreach (var dis in Model.GroupedTopicsDescriptions)
	   {%>
       
           <li class="disciplineName">
               <%: dis.Key %>
               <ul>
               <% foreach (var chapter in dis.Value)
				  {%>
                    <li class="chapterName"><%: chapter.Key %>
                    <ul class="ul">
                    <% foreach (var package in chapter.Value)
					   {%>
                         <li class="topicLink topicName">
                         
                         <% for (var i = 1; i <= 5; ++i)
							{ %>
                            <input name="rating_<%= package.Topic.Id + "_" + package.CurriculumChapterTopicId + "_" + package.CourseId + "_" + package.TopicType.ToString()%>" value="<%=i%>" <%= (package.Rating == i ? "checked='checked'" : "") %> <%= (package.Rating != 0 ? "disabled='disabled'" : "") %> type="radio" class="rating required"/>
                         <% } %>
                         
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
                    </ul>
                   </li>
               <% } %>
               </ul>
           </li>
       
  
       <% } %>
       </ul>
    </div>
	<% } %>
	
    <%: Html.Action("RecommendedTopics", "Recommender")%>
</asp:Content>
