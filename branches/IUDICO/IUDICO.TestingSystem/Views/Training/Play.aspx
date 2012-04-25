<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.TestingSystem.ViewModels.PlayModel>" %>
<%@ Assembly Name="IUDICO.TestingSystem" %>
<%@ Import Namespace="System.Web.DynamicData" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls.Expressions" %>
<%@ Import Namespace="System.Web.UI.WebControls.WebParts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.TestingSystem.Localization.GetMessage("PlayCourse")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ShowResults" style="display: none;"><%:
                Html.RouteLink(
                    "Show Results",
                    new
                        {
                            controller = "Stats",
                            action = "CurrentTopicTestResults",
                            curriculumChapterTopicId = Model.CurriculumChapterTopicId,
                            topicType = Model.TopicType
                        })%></div>
    <iframe width="100%" height="600px" frameborder="0" src="<%:"/Player/Frameset/Frameset.aspx?View=0&AttemptId=" + Model.AttemptId.ToString()%>" id="player" name="player" style="display: block;"></iframe>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        var viewPortHeight;
        var viewPortOffsetBottom;
        var viewPortOffsetTop;

        function SubmitTraining() {
            document.location = $("#ShowResults").children("a")[0].href;
        }

        $(function() {
            if (typeof window.innerWidth != 'undefined') {
                viewPortHeight = window.innerHeight;
            }

                // IE6 in standards compliant mode (i.e. with a valid doctype as the first line in the document)

            else if (typeof document.documentElement != 'undefined' && typeof document.documentElement.clientWidth != 'undefined' && document.documentElement.clientWidth != 0) {
                viewPortHeight = document.documentElement.clientHeight;
            }

                // older versions of IE

            else {
                viewPortHeight = document.getElementsByTagName('body')[0].clientHeight;
            }

            viewPortOffsetBottom = parseInt(window.getComputedStyle(document.getElementById("main"), null).getPropertyValue("margin-bottom"));

            viewPortOffsetTop = document.getElementById("main").offsetTop;
        });
    </script>
    <style type="text/css">
    	#main
    	{
    		padding: 0;
    	}

    	#footer
    	{
    		padding: 0;
    		margin: 0;
    		height: 0;
    	}
    </style>
</asp:Content>