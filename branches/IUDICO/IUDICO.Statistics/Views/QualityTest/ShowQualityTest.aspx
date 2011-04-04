<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.QualityTest.ShowQualityTestModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ShowQualityTest
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ShowQualityTest</h2>
    <fieldset>
        <div>
        Teacher: <%: Model.GetTeacherUserName()%>
        </div>
        <div>
        Curriculum: <%: Model.GetCurriculumName()%>
        </div>
        <div>
        Theme: <%: Model.GetThemeName()%>
        </div>
        <table>
            <tr>
                <th>Number of question</th>
                <th>Diagram</th>
                <th>Coefficient</th>
            </tr>
            <%foreach (KeyValuePair<int, double> question in Model.GetListOfCoefficient())
              {%>
              <tr>
              <td><%: question.Key %></td>
                <% string diagram = "*";
                foreach (IUDICO.Statistics.Models.QualityTest.UserAnswers userAnswer in Model.GetListOfUserAnswers())
                {
                    if (userAnswer.GetUserScoreForTest(question.Key) > 0)
                        diagram += '|';
                    else
                        diagram += ' ';
                } %>
                <td><%:diagram%></td>
                <td><%: question.Value %></td>
              </tr>
              <%} %>
        </table>
    </fieldset>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
