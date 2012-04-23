<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Group>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Select Group
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Select Group</h2>
    <%if (Model.Count() != 0)
      {%>
    <form action="/AnomalyDetection/TrainTopic/" method="get">
     <table>
        <tr>
            <th>Group Name</th>
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
              <input name="groupId" type="radio" value="<%: item.Id %>"/>
            </td>
        </tr>
     <%
          }%>
     </table>
     <p>
        <input type="submit" value="Select" />
    </p>
    </form>
    <%}
      else
      { %>
      No groups available for this topic.
      <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
