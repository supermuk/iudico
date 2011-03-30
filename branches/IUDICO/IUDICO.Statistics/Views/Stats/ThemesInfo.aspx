<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.ThemeInfoModel>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ThemesInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <%: Html.ActionLink(StatisRes.Statistics.Back, "Index")%>
    <fieldset>
    <legend> <%=StatisRes.Statistics.SelectAttempt%></legend>
    <form action="/Stats/ThemeTestResaults/" method="post">
        <table border="4" cellpadding="4" cellspacing="4">
        
        <tr>
        <th> <%=StatisRes.Statistics.Student%> </th>
        <% foreach (IUDICO.Common.Models.Theme i in Model.SelectCurriculumThemes)
           { %>
        <th> <%: i.Name %> </th>
        <% } %>
        <th> <%=StatisRes.Statistics.Sum%> </th>
        <th> <%=StatisRes.Statistics.Percent%> </th>
        <th> ECTS </th>
        </tr>

        <% foreach (IUDICO.Common.Models.User student in Model.SelectStudents)
           {  %>

            <tr> 
                <td> <%: student.Username%></td>
                <% foreach (IUDICO.Common.Models.Theme selectTheme in Model.SelectCurriculumThemes) 
                    { %>
                        <td>
                        <input type="radio" name="attemptUsernameAndTheme" value="<%: student.Username+selectTheme.Name %>" checked="checked" />
                            <%:
                                    Model.GetStudentResultForTheme(student, selectTheme).ToString() +
                                    "/" + Model.GetMaxResautForTheme(selectTheme).ToString()
                                %>
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

        <input type="submit" value=<%=StatisRes.Statistics.Show %> />


        </form>
    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
