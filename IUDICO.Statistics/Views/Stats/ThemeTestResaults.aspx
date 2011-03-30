<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.ThemeTestResaultsModel>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ThemeTestResaults
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink(StatisRes.Statistics.Back, "Index")%>
    <fieldset>
    <legend> <%=StatisRes.Statistics.AttemptStatistic%></legend>
        <h2><%=StatisRes.Statistics.Resaults%></h2>
        <p>
            <%=StatisRes.Statistics.Student%>:  <%: Model.Attempt.User.Username%>
        </p>
        <p>
            <%=StatisRes.Statistics.Theme%>:  <%: Model.Attempt.Theme.Name%>
        </p>
        <p>
            <%=StatisRes.Statistics.Success%>:  <%: Model.Attempt.SuccessStatus%>
        </p>
        <p>
            <%=StatisRes.Statistics.Score%>:  <%: Model.Attempt.Score.ToPercents()%>
        </p>

        <table border="4" cellpadding="4" cellspacing="4">
        
        <tr>
        <th><%=StatisRes.Statistics.NumberOfQuestion%> </th>
        <th> <%=StatisRes.Statistics.StudentAnswer%> </th>
        <th> <%=StatisRes.Statistics.CorectAnswer%> </th>
        <th> <%=StatisRes.Statistics.Comparison%></th>
        <th> <%=StatisRes.Statistics.Score%> </th>
        </tr>
        <%int i = 1; %>
        <% foreach (IUDICO.Common.Models.Shared.Statistics.AnswerResult answer in Model.UserAnswers)
           {  %>
           <tr>
                <td><%:i++ %></td>
                <td><%: Model.GetUserAnswer(answer)%></td>
                <td><%: answer.CorrectResponse%></td>
                <td><%if (Model.GetUserAnswer(answer) == answer.CorrectResponse)
                      {%>
                      true
                      <%}
                      else if (Model.GetUserAnswer(answer) == "")
                      { %>
                       
                      <% 
                      }
                      else
                      { %>
                      false
                      <%} %>
                </td>
                <td><%: Model.GetUserScoreForAnswer(answer)%></td>
           </tr>

        <% } %>
        </table>
    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>