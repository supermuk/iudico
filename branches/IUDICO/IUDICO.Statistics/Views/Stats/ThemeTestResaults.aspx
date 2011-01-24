<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.ThemeTestResaultsModel>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ThemeTestResaults
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink("<- back", "Index") %>
    <fieldset>
    <legend> Attempt statistic:</legend>
        <h2>Resaults</h2>
        <p>
            Student:  <%: Model.Attempt.User.Username%>
        </p>
        <p>
            Theme:  <%: Model.Attempt.Theme.Name%>
        </p>
        <p>
            Success:  <%: Model.Attempt.SuccessStatus%>
        </p>
        <p>
            Score:  <%: Model.Attempt.Score.ToPercents()%>
        </p>

        <table border="4" cellpadding="4" cellspacing="4">
        
        <tr>
        <th> Number of question </th>
        <th> Student Answer </th>
        <th> Corect Answer </th>
        <th> Comparison</th>
        <th> Score </th>
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