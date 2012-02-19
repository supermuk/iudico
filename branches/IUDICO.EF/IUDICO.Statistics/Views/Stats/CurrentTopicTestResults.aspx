﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.StatisticsModels.CurrentTopicTestResultsModel>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.Statistics.Localization.getMessage("Results")%>
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
            <%=IUDICO.Statistics.Localization.getMessage("Topic")%>:  <%: Model.GetTopicName()%>
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
        </tr>
        <%int i = 1; %>
        <% foreach (IUDICO.Common.Models.Shared.Statistics.AnswerResult answer in Model.GetUserAnswers())
           {  %>
           <tr>
                <td><%:i++%></td>
                <td><%: Model.GetUserAnswer(answer)%></td>                
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