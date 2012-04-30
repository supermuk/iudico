<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.ForecastingTree>>" %>
<%@ Import Namespace="IUDICO.Analytics" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%:Html.ActionLink("Tags", "Index", "Tags")%> | <%:Html.ActionLink("Recommender", "Index", "Recommender")%> | <%:Html.ActionLink("Anomaly Detection", "Index", "AnomalyDetection")%>

    <h2>Hello Analytics</h2>

     <table>
     
     <tr>
        <th> </th>
        <th>Name</th>
        <th>Created</th>
        <th>Updated</th>
        <th>Edit</th>
        <th>Delete</th>
        <th>Teach</th>
        <th>Make forecasting</th>
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
