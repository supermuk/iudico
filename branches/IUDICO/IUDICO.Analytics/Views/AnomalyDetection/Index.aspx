<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Topic>>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Anomaly detection
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("AnomalyDetection")%></h2>
    <%if (Model.Count() != 0)
      {%>
    <table>
        <tr>
            <th><%=Localization.GetMessage("TopicName") %></th>
            <th></th>
        </tr>

     <%
          foreach (var item in Model)
          {%>
        <tr>
            <td>
            <%:item.Name%>
            </td>
            <td>
                <%:Html.ActionLink(Localization.GetMessage("TrainTopic"), "SelectGroup", new { topicId = item.Id })%>
            </td>
        </tr>
     <%
          }%>

     </table>
     <%}
      else
      { %>
      <%=Localization.GetMessage("AnomalyOwnershipWarning")%> 
     <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
