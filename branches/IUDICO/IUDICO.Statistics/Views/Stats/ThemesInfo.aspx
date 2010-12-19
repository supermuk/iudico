<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.ThemeInfoModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ThemesInfo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <%: Html.ActionLink("<- back", "Index") %>
    <fieldset>
    <legend>Статистика :</legend>

        <table border="4" cellpadding="4" cellspacing="4">

        <tr>
        <th>Студент </th>
        <% foreach (IUDICO.Statistics.Models.Theme i in Model.SelectCurriculumThemes)
           { %>
        <th> <%: i.Name %> </th>
        <% } %>
        <th> Загальні бали </th>
        <th> Відсотки </th>
        <th> Оцінка ECTS</th>
        </tr>

        <% foreach (IUDICO.Statistics.Models.Student student in Model.SelectStudents)
           {  %>

            <tr> 
            <td><%: student.Name%></td>
            <% foreach (IUDICO.Statistics.Models.Theme selectTheme in Model.SelectCurriculumThemes) 
                { %>
                    <td>
                                <%: Html.ActionLink(Model.GetStudentResautForTheme(student,selectTheme).ToString() + "/" + selectTheme.MaxPoint.ToString()
                                                 , "ThemeTestResaults", new { StudentID = student.StudentId, ThemeID = selectTheme.ThemeId })%>
                    </td>
                <% } %>  
            
            <td>
             <%:Model.GetStudentResautForAllThemesInSelectedCurriculum(student) %>
              / 
             <%: Model.GetAllThemesInSelectedCurriculumMaxMark()%> 
            </td>
            <td> <%:Math.Round(
                     ((double)Model.GetStudentResautForAllThemesInSelectedCurriculum(student)) / ((double) Model.GetAllThemesInSelectedCurriculumMaxMark()) * 100
                                      ,2)%> </td>
            <td> <%:Model.Ects(
                     ((double)Model.GetStudentResautForAllThemesInSelectedCurriculum(student)) / ((double)Model.GetAllThemesInSelectedCurriculumMaxMark()) * 100
                                      )%></td>
            </tr>

        <% } %>

        </table>

    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
