<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Group>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SelectGroup
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>SelectGroup</h2>
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
            <%:item.Name %>
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
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
