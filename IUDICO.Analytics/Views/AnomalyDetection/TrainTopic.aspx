<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<KeyValuePair<IUDICO.Common.Models.Shared.User,double[]>>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	TrainTopic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language=javascript>
        function selectTs1(id) {
            var ts2n = document.getElementsByName('ts2n');
            for (var i = 0; i < ts2n.length; i++) {
                if (ts2n.item(i).attributes.getNamedItem('value').nodeValue == id) {
                    ts2n.item(i).checked = false;
                }
            }
            var ts2a = document.getElementsByName('ts2a');
            for (var i = 0; i < ts2a.length; i++) {
                if (ts2a.item(i).attributes.getNamedItem('value').nodeValue == id) {
                    ts2a.item(i).checked = false;
                }
            }
        }
        function selectTs2n(id) {
            var ts1 = document.getElementsByName('ts1');
            for (var i = 0; i < ts1.length; i++) {
                if (ts1.item(i).attributes.getNamedItem('value').nodeValue == id) {
                    ts1.item(i).checked = false;
                }
            }
            var ts2a = document.getElementsByName('ts2a');
            for (var i = 0; i < ts2a.length; i++) {
                if (ts2a.item(i).attributes.getNamedItem('value').nodeValue == id) {
                    ts2a.item(i).checked = false;
                }
            }
        }
        function selectTs2a(id) {
            var ts1 = document.getElementsByName('ts1');
            for (var i = 0; i < ts1.length; i++) {
                if (ts1.item(i).attributes.getNamedItem('value').nodeValue == id) {
                    ts1.item(i).checked = false;
                }
            }
            var ts2n = document.getElementsByName('ts2n');
            for (var i = 0; i < ts2n.length; i++) {
                if (ts2n.item(i).attributes.getNamedItem('value').nodeValue == id) {
                    ts2n.item(i).checked = false;
                }
            }
        }
    </script>
    <h2>TrainTopic</h2>
    <form action="/AnomalyDetection/TrainAlg/" method="get">
     <table>
        <tr>
            <th>Student Name</th>
            <th>Student Score</th>
            <th>Student Time</th>
            <th>Training set</th>
            <th>CV set (normal)</th>
            <th>CV set (anomalies)</th>
        </tr>

     <%
         foreach (var item in Model)
         {%>
        <tr>
            <td>
            <%:item.Key.Name %>
            </td>
            <td>
            <%:Math.Round((double)item.Value[1], 2).ToString() %>
            </td>
            <td>
            <% int minutes = Convert.ToInt32(item.Value[0]) / 60;
               int seconds = Convert.ToInt32(item.Value[0]) % 60;
                 %>
            <%: minutes.ToString() + ':' + ((seconds < 10)?"0":"") + seconds.ToString()%>
            </td>
            <td>
              <input type="checkbox" value="<%: item.Key.OpenId %>" name="ts1" onchange="javascript:selectTs1(<%:item.Key.OpenId %>)"/>
            </td>
            <td>
              <input type="checkbox" value="<%: item.Key.OpenId %>" name="ts2n" onchange="javascript:selectTs2n(<%:item.Key.OpenId %>)"/>
            </td>
            <td>
              <input type="checkbox" value="<%: item.Key.OpenId %>" name="ts2a" onchange="javascript:selectTs2a(<%:item.Key.OpenId %>)"/>
            </td>
        </tr>
     <%
         }%>
     </table>
     <p>
        <input type="submit" value="next" />
    </p>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
