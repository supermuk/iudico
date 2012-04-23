<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Topic>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Anomaly detection
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Anomaly detection</h2>
    <%if (Model.Count() != 0)
      {%>
    <table>
        <tr>
            <th>Topic Name</th>
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
                <%:Html.ActionLink("Train topic", "SelectGroup", new { topicId = item.Id })%>
            </td>
        </tr>
     <%
          }%>

     </table>
     <%}
      else
      { %>
      Sorry, but you can use Anomaly Detection algorithm only to topics that you owned. 
     <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
