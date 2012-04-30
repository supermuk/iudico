<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<KeyValuePair<IUDICO.Common.Models.Shared.User,double[]>>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Train Topic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language=javascript>
        function selectTsNormal(id) {
            var ts2a = document.getElementsByName('tsAnomalies');
            for (var i = 0; i < ts2a.length; i++) {
                if (ts2a.item(i).attributes.getNamedItem('value').nodeValue == id) {
                    ts2a.item(i).checked = false;
                }
            }
        }
        function selectTsAnomalies(id) {
            var ts1 = document.getElementsByName('tsNormal');
            for (var i = 0; i < ts1.length; i++) {
                if (ts1.item(i).attributes.getNamedItem('value').nodeValue == id) {
                    ts1.item(i).checked = false;
                }
            }
        }
    </script>
    <h2>Train Topic</h2>
    <%if (ViewData["ShowError"] != null)
      { %>
      Please, select valid training set!<br /> 
      Check selected values for errors: if two records have same value of any feature (for example score),<br />
      then they can not be in different classes (anomalies and normal).<br />
      <%} %>
    <br />
    <form action="/AnomalyDetection/TrainAlg/" method="get">
     <table>
        <tr>
            <th>Student Name</th>
            <th>Student Score</th>
            <th>Student Time</th>
            <th>Normal</th>
            <th>Anomalies</th>
        </tr>

     <%
         foreach (var item in Model)
         {%>
        <tr>
            <td>
            <%:item.Key.Name %>
            </td>
            <td>
            <%:Math.Round((double)item.Value[0], 2).ToString() %>
            </td>
            <td>
            <% int minutes = Convert.ToInt32(item.Value[1]) / 60;
               int seconds = Convert.ToInt32(item.Value[1]) % 60;
                 %>
            <%: minutes.ToString() + ':' + ((seconds < 10)?"0":"") + seconds.ToString()%>
            </td>
            <td>
              <input type="checkbox" value="<%: item.Key.OpenId %>" name="tsNormal" onchange="javascript:selectTsNormal(<%:item.Key.OpenId %>)"/>
            </td>
            <td>
              <input type="checkbox" value="<%: item.Key.OpenId %>" name="tsAnomalies" onchange="javascript:selectTsAnomalies(<%:item.Key.OpenId %>)"/>
            </td>
        </tr>
     <%
         }%>
     </table>
     <p>
        <input type="submit" value="Train" />
    </p>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
