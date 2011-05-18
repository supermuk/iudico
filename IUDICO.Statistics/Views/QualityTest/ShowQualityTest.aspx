<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.QualityTest.ShowQualityTestModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ShowQualityTest
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ShowQualityTest page</h2>
    <fieldset>
    <legend> Results of quality test algorithm</legend>
    <%if (Model.NoData() == false)
      { %>
        <p>
        Curriculum: <%: Model.GetCurriculumName()%>
        </p>
        <p>
        Theme: <%: Model.GetThemeName()%>
        </p>
        <table>
            <tr>
                <th>Number of question</th>
                <th>Number of students</th>
                <th>Coefficient</th>
            </tr>
            <%foreach (IUDICO.Statistics.Models.QualityTest.ShowQualityTestModel.QuestionModel question in Model.GetListOfQuestionModels())
              {%>
              <tr>
                <td><%: question.GetQuestionNumber()%></td>
                <td><%: question.GetNumberOfStudents()%></td>
                <td>
                <% if (question.NoData() != true)
                   {%>
                    <%: Math.Round(question.GetCoefficient(), 2)%>
                <%}
                   else
                   { %>
                   NoData
                   <%} %>
                </td>
              </tr>
              <%} %>
        </table>
        <%}
      else
      { %>
      No data to show
      <%} %>
    </fieldset>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
