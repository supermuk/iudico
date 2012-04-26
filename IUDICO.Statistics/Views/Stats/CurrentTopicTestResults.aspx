<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.StatisticsModels.CurrentTopicTestResultsModel>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Import Namespace="IUDICO.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("Results")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
    <%if (Model.NoData() == true)
      {%>
        <%=Localization.GetMessage("NoDataShow")%>
        <%
        }
      else
      {%>
        <legend> <%=Localization.GetMessage("AttemptStatistic")%></legend>
        <h2><%=Localization.GetMessage("Results")%></h2>
        <p>
            <%=Localization.GetMessage("Student")%>:  <%: Model.GetUserName()%>
        </p>
        <p>
            <%=Localization.GetMessage("Topic")%>:  <%: Model.GetTopicName()%>
        </p>
        <p>
            <%=Localization.GetMessage("Success")%>:  <%: Model.GetSuccessStatus()%>
        </p>
        <p>
            <%=Localization.GetMessage("Score")%>:  <%: Model.GetScore()%>
        </p>

        <table border="4" cellpadding="4" cellspacing="4">
        
        <tr>
        <th><%=Localization.GetMessage("NumberOfQuestion")%> </th>
        <th> <%=Localization.GetMessage("StudentAnswer")%> </th>
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