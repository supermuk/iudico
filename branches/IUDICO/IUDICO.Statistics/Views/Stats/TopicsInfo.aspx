<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.StatisticsModels.TopicInfoModel>" %>

<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Import Namespace="IUDICO.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("TopicsInfo")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.ActionLink(Localization.GetMessage("Back"), "Index")%>
    <fieldset>
        <legend>
            <%=Localization.GetMessage("SelectAttempt")%></legend>
        <table id="topicsTable">
            <thead>
                <tr>
                    <th>
                        <%=Localization.GetMessage("Student")%>
                    </th>
                    <% foreach (var curriculumChapterTopic in Model.SelectedCurriculumChapterTopics)
                       { %>
                    <th>
                        <%: curriculumChapterTopic.Topic.Name%>
                    </th>
                    <% } %>
                    <th>
                        <%=Localization.GetMessage("Sum")%>
                    </th>
                    <th>
                        <%=Localization.GetMessage("Percent")%>
                    </th>
                    <th>
                        ECTS
                    </th>
                </tr>
            </thead>
            <tbody>
                <% int i = 0;
                   foreach (var student in Model.SelectedStudents)
                   {  %>
                <tr>
                    <td>
                        <%: student.Name%>
                    </td>
                    <%  foreach (var curriculumChapterTopic in Model.SelectedCurriculumChapterTopics)
                        {
                            i++;
                    %>
                    <td>
                        <%if (Model.ContainsResult(student, curriculumChapterTopic))
                          { %>
                        <form name="linkform<%:i%>" action="/Stats/TopicTestResults/" method="post">
                        <input type="hidden" name="attemptId" value="<%: Model.GetAttempId(student, curriculumChapterTopic)%>" />
                        </form>
                        <a href="javascript:document.forms['linkform<%:i%>'].submit();">
                            <%: Model.GetStudentResultForTopic(student, curriculumChapterTopic)%>
                            /
                            <%: Model.GetMaxResutForTopic(student, curriculumChapterTopic) %>
                        </a>
                        <%}
                          else
                          {%>
                        <%=Localization.GetMessage("NoData")%>
                        <%} %>
                    </td>
                    <% } %>
                    <td>
                        <%: Model.GetStudentResultForAllTopicsInSelectedDiscipline(student)%>
                        /
                        <%: Model.GetAllTopicsInSelectedDisciplineMaxMark(student)%>
                    </td>
                    <td>
                        <%: Math.Round(Model.GetPercentScore(student)) %>
                        %
                    </td>
                    <td>
                        <%:Model.Ects(Math.Round(Model.GetPercentScore(student)))%>
                    </td>
                </tr>
                <% } %>
            </tbody>
        </table>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .dataTables_wrapper
        {
        	min-height: 100px;
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#topicsTable").dataTable({
                "bJQueryUI": true,
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": true,
                "bSort": false,
                "bInfo": false,
                "bAutoWidth": true
            });
        });
    </script>
</asp:Content>
