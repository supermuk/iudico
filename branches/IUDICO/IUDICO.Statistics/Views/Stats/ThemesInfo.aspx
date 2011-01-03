<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.ThemeInfoModel>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ThemesInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <%: Html.ActionLink("<- back", "Index") %>
    <fieldset>
    <legend> Please, select attempt to show:</legend>
    <form action="/Stats/ThemeTestResaults/" method="post">
        <table border="4" cellpadding="4" cellspacing="4">
        
        <tr>
        <th> Student </th>
        <% foreach (IUDICO.Common.Models.Theme i in Model.SelectCurriculumThemes)
           { %>
        <th> <%: i.Name %> </th>
        <% } %>
        <th> Sum </th>
        <th> Percent </th>
        <th> ECTS </th>
        </tr>

        <% foreach (IUDICO.Common.Models.User student in Model.SelectStudents)
           {  %>

            <tr> 
                <td> <%: student.Name %></td>
                <% foreach (IUDICO.Common.Models.Theme selectTheme in Model.SelectCurriculumThemes) 
                    { %>
                        <td>
                        <input type="radio" name="AttemptId" value="<%: Model.GetStudentAttempt(student,selectTheme).AttemptID %>"/>
                            <%:
                                    Model.GetStudentResautForTheme(student, selectTheme).ToString() + 
                                    "/" + Model.GetMaxResautForTheme(selectTheme).ToString()
                                %>
                        </td>
                    <% } %>  
            
                <td>
                 <%:Model.GetStudentResautForAllThemesInSelectedCurriculum(student) %>
                  / 
                 <%: Model.GetAllThemesInSelectedCurriculumMaxMark()%> 
                </td>
                <td> <%:Model.GetStudentResautForAllThemesInSelectedCurriculum(student)/Model.GetAllThemesInSelectedCurriculumMaxMark() * 100%> %
                </td>
                <td> <%:Model.Ects
                         (
                            (Model.GetStudentResautForAllThemesInSelectedCurriculum(student))/(Model.GetAllThemesInSelectedCurriculumMaxMark())
                          )%>
                </td>
            </tr>

        <% } %>

        </table>

        <input type="submit" value="Show" />


        </form>
    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
