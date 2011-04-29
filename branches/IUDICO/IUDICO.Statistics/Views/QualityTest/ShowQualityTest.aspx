<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.QualityTest.ShowQualityTestModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ShowQualityTest
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ShowQualityTest</h2>
    <fieldset>
    <%if (Model.NoData() == false)
      { %>
        <div>
        Curriculum: <%: Model.GetCurriculumName()%>
        </div>
        <div>
        Theme: <%: Model.GetThemeName()%>
        </div>
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
                <td><%:question.GetNumberOfStudents()%></td>
                <td><%: question.GetCoefficient()%></td>
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
