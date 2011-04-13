<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Curriculum>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SelectCurriculums

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        function checkBox() {
            if ($('input:checkbox:checked').length == 0) {
                alert('Please, select one or more curriculum')
            }
            else {
                $('#curform').submit();
            }
        }
    </script>

    <h2><%=StatisRes.Statistics.CurriculumList %> for <%: ViewData["Group"] %> </h2>


     <%: Html.ActionLink(StatisRes.Statistics.Back, "Index")%>
     <fieldset>

     <legend><%=StatisRes.Statistics.SelectCurriculum%> </legend>


    <form id="curform" action="/Stats/ShowCurriculumStatistic/" method="post">
     <table>
     
     <tr>
        <th> </th>
        <%--<th>Curriculum id</th>--%>
        <th><%=StatisRes.Statistics.CurriculumName%></th>
        <th><%=StatisRes.Statistics.Created%></th>
     </tr>

     <% foreach (IUDICO.Common.Models.Curriculum curr in Model)
        { %>
        <tr>
            <td>
            <input type="checkbox" name="selectCurriculumId" value="<%: curr.Id %>" id="<%: curr.Id %>" />
            </td>
            <%--<td>
            <%: curr.Id %>
            </td>--%>
            <td>
            <%: curr.Name %>
            </td>
            <td>
            <%: curr.Created %>
            </td>
        </tr>
     <% } %>

     </table>

     <input type="button" value=<%=StatisRes.Statistics.Show %> onclick="checkBox();" />

</form>

<!--
<a href="#" onclick="isChecked();">Show</a>
-->
     </fieldset>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">


</asp:Content>
