<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<KeyValuePair<KeyValuePair<IUDICO.Common.Models.Shared.User,double[]>,bool>>>" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>
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
            <%foreach (Tag tag in (IEnumerable<Tag>)ViewData["SkillTags"])
              {%>
              <th><%:tag.Name %></th>
              <%} %>
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
            <%:Math.Round(item.Key.Value[0], 2).ToString()%>
            </td>
            <td>
            <% int minutes = Convert.ToInt32(item.Key.Value[1]) / 60;
               int seconds = Convert.ToInt32(item.Key.Value[1]) % 60;
                 %>
            <%: minutes.ToString() + ':' + ((seconds < 10)?"0":"") + seconds.ToString()%>
            </td>
            <%for (int i = 2; i < item.Key.Value.Length; i++)
              { %>
                <td>
                    <%:Math.Round(item.Key.Value[i], 2).ToString()%>
                </td>
            <%} %>
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
