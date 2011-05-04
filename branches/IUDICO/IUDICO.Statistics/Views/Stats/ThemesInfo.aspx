<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.StatisticsModels.ThemeInfoModel>" %>
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
        <% foreach (IUDICO.Common.Models.Theme theme in Model.GetSelectCurriculumThemes())
           { %>
        <th> <%: theme.Name%> </th>
        <% } %>
        <th> <%=IUDICO.Statistics.Localization.getMessage("Sum")%> </th>
        <th> <%=IUDICO.Statistics.Localization.getMessage("Percent")%> </th>
        <th> ECTS </th>
        </tr>

        <% int i = 0;
           foreach (IUDICO.Common.Models.User student in Model.GetSelectStudents())
           {  %>

            <tr> 
                <td> <%: student.Name%></td>
                <%  foreach (IUDICO.Common.Models.Theme selectTheme in Model.GetSelectCurriculumThemes()) 
                    {
                        i++;
                        %>                          
                        <td>
                        <%if (Model.NoData(student, selectTheme) == false)
                        { %>
                        <form name="linkform<%:i%>" action="/Stats/ThemeTestResults/" method="post">
                        <input type="hidden" name="attemptId" value="<%: Model.GetAttempId(student, selectTheme)%>"/>
                        </form>
                        <a href="javascript:document.forms['linkform<%:i%>'].submit();">                     
                            <%:
                                    Model.GetStudentResultForTheme(student, selectTheme).ToString() +
                                    "/" + Model.GetMaxResutForTheme(selectTheme).ToString()
                                %>
                        </a>
                        <%}
                        else
                        {%>
                            <%=IUDICO.Statistics.Localization.getMessage("NoData")%> 
                        <%} %> 
                        </td>
                    <% } %>  
            
                <td>
                 <%: Model.GetStudentResultForAllThemesInSelectedCurriculum(student)%>
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
