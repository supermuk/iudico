<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<KeyValuePair<KeyValuePair<IUDICO.Common.Models.Shared.User,IUDICO.Common.Models.Shared.Statistics.AttemptResult>,bool>>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	TrainAlg
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Algorithm Output</h2>
     <table>
        <tr>
            <th>Student Name</th>
            <th>Student Score</th>
            <th>Student Time</th>
            <th></th>
        </tr>

     <%
         foreach (var item in Model)
         {%>
        <tr>
            <td>
            <%:item.Key.Key.Name%>
            </td>
            <td>
            <%:Math.Round((double)item.Key.Value.Score.ToPercents(), 2).ToString()%>
            </td>
            <td>
            <%: item.Key.Value.FinishTime.Value.Subtract(item.Key.Value.StartTime.Value).ToString()%>
            </td>
            <td>
            <% if (item.Value)
               {%>
               Anomaly
            <%} %>
            </td>
        </tr>
     <%
         }%>
     </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
