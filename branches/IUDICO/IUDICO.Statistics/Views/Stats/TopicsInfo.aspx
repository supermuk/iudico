<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.StatisticsModels.TopicInfoModel>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Import Namespace="IUDICO.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("TopicsInfo")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
    <legend> <%=Localization.GetMessage("SelectAttempt")%></legend>
        <table border="4" cellpadding="4" cellspacing="4">
        
        <tr>
        <th> <%=Localization.GetMessage("Student")%> </th>
        <% foreach (IUDICO.Common.Models.Shared.Topic topic in Model.GetSelectDisciplineTopics())
           { %>
        <th> <%: topic.Name%> </th>
        <% } %>
        <th> <%=Localization.GetMessage("Sum")%> </th>
        <th> <%=Localization.GetMessage("Percent")%> </th>
        <th> ECTS </th>
        </tr>

        <% int i = 0;
           foreach (IUDICO.Common.Models.Shared.User student in Model.GetSelectStudents())
           {  %>

            <tr> 
                <td> <%: student.Name%></td>
                <%  foreach (IUDICO.Common.Models.Shared.Topic selectTopic in Model.GetSelectDisciplineTopics()) 
                    {
                        i++;
                        %>                          
                        <td>
                        <%if (Model.NoData(student, selectTopic) == false)
                        { %>
                        <form name="linkform<%:i%>" action="/Stats/TopicTestResults/" method="post">
                        <input type="hidden" name="attemptId" value="<%: Model.GetAttempId(student, selectTopic)%>"/>
                        </form>
                        <a href="javascript:document.forms['linkform<%:i%>'].submit();">                     
                            <%:
                                    Model.GetStudentResultForTopic(student, selectTopic).ToString() +
                                    "/" + Model.GetMaxResutForTopic(selectTopic).ToString()
                                %>
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
                 <%: Model.GetAllTopicsInSelectedDisciplineMaxMark()%> 
                </td>
                <td> <%:Model.GetStudentResultForAllTopicsInSelectedDiscipline(student) / Model.GetAllTopicsInSelectedDisciplineMaxMark() * 100%> %
                </td>
                <td> <%:Model.Ects
                         (
                         Model.GetStudentResultForAllTopicsInSelectedDiscipline(student) / Model.GetAllTopicsInSelectedDisciplineMaxMark() * 100
                          )%>
                </td>
            </tr>

        <% } %>

        </table>
    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
