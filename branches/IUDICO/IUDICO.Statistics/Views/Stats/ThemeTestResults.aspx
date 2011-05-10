<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.StatisticsModels.ThemeTestResultsModel>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ThemeTestResults
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
    <%if (Model.NoData() == true)
      {%>
        <%=IUDICO.Statistics.Localization.getMessage("NoDataShow")%>
        <%
        }
      else
      {%>
        <legend> <%=IUDICO.Statistics.Localization.getMessage("AttemptStatistic")%></legend>
        <h2><%=IUDICO.Statistics.Localization.getMessage("Results")%></h2>
        <p>
            <%=IUDICO.Statistics.Localization.getMessage("Student")%>:  <%: Model.GetUserName()%>
        </p>
        <p>
            <%=IUDICO.Statistics.Localization.getMessage("Theme")%>:  <%: Model.GetThemeName()%>
        </p>
        <p>
            <%=IUDICO.Statistics.Localization.getMessage("Success")%>:  <%: Model.GetSuccessStatus()%>
        </p>
        <p>
            <%=IUDICO.Statistics.Localization.getMessage("Score")%>:  <%: Model.GetScore()%>
        </p>

        <table border="4" cellpadding="4" cellspacing="4">
        
        <tr>
        <th><%=IUDICO.Statistics.Localization.getMessage("NumberOfQuestion")%> </th>
        <th> <%=IUDICO.Statistics.Localization.getMessage("StudentAnswer")%> </th>
        <th> <%=IUDICO.Statistics.Localization.getMessage("CorrectAnswer")%> </th>
        <th> <%=IUDICO.Statistics.Localization.getMessage("Comparison")%></th>
        <th> <%=IUDICO.Statistics.Localization.getMessage("Score")%> </th>
        </tr>
        <%int i = 1; %>
        <% foreach (IUDICO.Common.Models.Shared.Statistics.AnswerResult answer in Model.GetUserAnswers())
           {  %>
           <tr>
                <td><%:i++%></td>
                <td><%: Model.GetUserAnswer(answer)%></td>
                <td><%: answer.CorrectResponse%></td>
                <td><%if (Model.GetUserAnswer(answer) == answer.CorrectResponse)
                      {%>
                            true
                      <%}
                      else if (Model.GetUserAnswer(answer) == "")
                      { %>
                            <%=IUDICO.Statistics.Localization.getMessage("NoData")%>
                      <% 
                      }
                      else
                      { %>
                            false
                      <%} %>
                </td>
                <td><%: Model.GetUserScoreForAnswer(answer)%></td>
           </tr>

        <% }
         %>
        </table>
        <%
      }%>
    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>