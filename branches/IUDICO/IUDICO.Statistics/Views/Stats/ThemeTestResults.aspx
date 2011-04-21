<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.ThemeTestResultsModel>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ThemeTestResaults
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
    <%if (Model.NoData() == true)
      {%>
        No data to show
        <%
        }
      else
      {%>
        <legend> <%=StatisRes.Statistics.AttemptStatistic%></legend>
        <h2><%=StatisRes.Statistics.Resalts%></h2>
        <p>
            <%=StatisRes.Statistics.Student%>:  <%: Model.GetUserName()%>
        </p>
        <p>
            <%=StatisRes.Statistics.Theme%>:  <%: Model.GetThemeName()%>
        </p>
        <p>
            <%=StatisRes.Statistics.Success%>:  <%: Model.GetSuccessStatus()%>
        </p>
        <p>
            <%=StatisRes.Statistics.Score%>:  <%: Model.GetScore()%>
        </p>

        <table border="4" cellpadding="4" cellspacing="4">
        
        <tr>
        <th><%=StatisRes.Statistics.NumberOfQuestion%> </th>
        <th> <%=StatisRes.Statistics.StudentAnswer%> </th>
        <th> <%=StatisRes.Statistics.CorrectAnswer%> </th>
        <th> <%=StatisRes.Statistics.Comparison%></th>
        <th> <%=StatisRes.Statistics.Score%> </th>
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
                            No data
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