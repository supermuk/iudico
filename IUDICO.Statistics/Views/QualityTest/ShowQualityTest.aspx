<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.QualityTest.ShowQualityTestModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.Statistics.Localization.getMessage("QualityTest")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.Statistics.Localization.getMessage("QualityTest")%></h2>
    <fieldset>
    <legend><%=IUDICO.Statistics.Localization.getMessage("Results")%></legend>
    <%if (Model.NoData() == false)
      { %>
        <p>
        <%=IUDICO.Statistics.Localization.getMessage("Curriculum")%>: <%: Model.GetCurriculumName()%>
        </p>
        <p>
        <%=IUDICO.Statistics.Localization.getMessage("Theme")%>: <%: Model.GetThemeName()%>
        </p>
        <table>
            <tr>
                <th><%=IUDICO.Statistics.Localization.getMessage("NumberOfQuestion")%></th>
                <th><%=IUDICO.Statistics.Localization.getMessage("NumberOfStudents")%></th>
                <th><%=IUDICO.Statistics.Localization.getMessage("Coefficient")%></th>
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
                   <%=IUDICO.Statistics.Localization.getMessage("NoData")%>
                   <%} %>
                </td>
              </tr>
              <%} %>
        </table>
        <%}
      else
      { %>
      <%=IUDICO.Statistics.Localization.getMessage("NoDataShow")%>
      <%} %>
    </fieldset>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
