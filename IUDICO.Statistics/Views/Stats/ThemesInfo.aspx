<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.ThemeInfoModel>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ThemesInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
    <legend> <%=IUDICO.Statistics.Localization.getMessage("SelectAttempt")%></legend>
        <table border="4" cellpadding="4" cellspacing="4">
        
        <tr>
        <th> <%=IUDICO.Statistics.Localization.getMessage("Student")%> </th>
        <% foreach (IUDICO.Common.Models.Theme i in Model.SelectCurriculumThemes)
           { %>
        <th> <%: i.Name %> </th>
        <% } %>
        <th> <%=IUDICO.Statistics.Localization.getMessage("Sum")%> </th>
        <th> <%=IUDICO.Statistics.Localization.getMessage("Percent")%> </th>
        <th> ECTS </th>
        </tr>

        <% foreach (IUDICO.Common.Models.User student in Model.SelectStudents)
           {  %>

            <tr> 
                <td> <%: student.Username%></td>
                <% 
                    int i = 0;
                    foreach (IUDICO.Common.Models.Theme selectTheme in Model.SelectCurriculumThemes) 
                    {
                        i++;
                       %>
                        <td>
                        <% long attemptId = Model.GetStudentAttemptId(student,selectTheme);
                        if (attemptId !=-1)
                        { %>
                        <form name="linkform<%:i%>" action="/Stats/ThemeTestResaults/" method="post">
                        <input type="hidden" name="attemptId" value="<%: attemptId%>"/>
                        </form>
                        <a href="javascript:document.forms['linkform<%:i%>'].submit();">                     
                            <%:
                                    Model.GetStudentResultForTheme(student, selectTheme).ToString() +
                                    "/" + Model.GetMaxResautForTheme(selectTheme).ToString()
                                %>
                        </a>
                        <%}
                        else
                        {%>
                            No data
                        <%} %> 
                        </td>
                    <% } %>  
            
                <td>
                 <%:Model.GetStudentResultForAllThemesInSelectedCurriculum(student)%>
                  / 
                 <%: Model.GetAllThemesInSelectedCurriculumMaxMark()%> 
                </td>
                <td> <%:Model.GetStudentResultForAllThemesInSelectedCurriculum(student) / Model.GetAllThemesInSelectedCurriculumMaxMark() * 100%> %
                </td>
                <td> <%:Model.Ects
                         (
                            (Model.GetStudentResultForAllThemesInSelectedCurriculum(student)) / (Model.GetAllThemesInSelectedCurriculumMaxMark())
                          )%>
                </td>
            </tr>

        <% } %>

        </table>
    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
