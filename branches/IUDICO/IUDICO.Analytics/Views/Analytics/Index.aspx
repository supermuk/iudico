<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.ForecastingTree>>" %>
<%@ Import Namespace="IUDICO.Analytics" %>
<%@ Import Namespace="IUDICO.Common" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%:Html.ActionLink("Tags", "Index", "Tags")%> | <%:Html.ActionLink("Recommender", "Index", "Recommender")%> | <%:Html.ActionLink("Anomaly Detection", "Index", "AnomalyDetection")%> | <%:Html.ActionLink("Discipline/Topic Quality", "Index", "Quality")%>

    <h2><%=Localization.GetMessage("HelloAnalytics")%></h2>

     <table>
     
     <tr>
        <th> </th>
        <th><%=Localization.GetMessage("Name")%></th>
        <th><%=Localization.GetMessage("Created") %></th>
        <th><%=Localization.GetMessage("Updated") %></th>
        <th><%=Localization.GetMessage("Edit") %></th>
        <th><%=Localization.GetMessage("Delete") %></th>
        <th><%=Localization.GetMessage("Teach") %></th>
        <th><%=Localization.GetMessage("MakeForecasting") %></th>
     </tr>

     <%
         foreach (ForecastingTree tree in Model)
         {%>
        <tr>
            <td>
            <input type="checkbox" name="selectTreeId" value="<%:tree.Id%>" id="<%:tree.Id%>" />
            </td>
            <td>
            <%:tree.Name%>
            </td>
            <td>
            <%:tree.Created%>
            </td>
            <td>
            <%:tree.Updated%>
            </td>
            <td>edit link</td>
            <td>delete link</td>
            <td>teach link</td>
            <td>make forecasting link</td>
        </tr>
     <%
         }%>

     </table>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
