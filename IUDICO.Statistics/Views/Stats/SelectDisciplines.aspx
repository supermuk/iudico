<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Curriculum>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.Statistics.Localization.getMessage("Statistics")%>: <%=IUDICO.Statistics.Localization.getMessage("DisciplineList")%> <%: ViewData["Group"]%>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        function checkBox() {
            if ($('input:checkbox:checked').length == 0) {
                alert('<%=IUDICO.Statistics.Localization.getMessage("SelectDiscipline")%>')
            }
            else {
                $('#curform').submit();
            }
        }
    </script>

    <% if (Model.Count() != 0)
       { %>

    <h2><%=IUDICO.Statistics.Localization.getMessage("DisciplineList")%> <%: ViewData["Group"]%> </h2>


     <%: Html.ActionLink(IUDICO.Statistics.Localization.getMessage("Back"), "Index")%>
     <fieldset>

     <legend><%=IUDICO.Statistics.Localization.getMessage("SelectDiscipline")%>: </legend>


    <form id="curform" action="/Stats/ShowDisciplineStatistic/" method="post">
     <table>
     
     <tr>
        <th> </th>
        <%--<th>Discipline id</th>--%>
        <th><%=IUDICO.Statistics.Localization.getMessage("DisciplineName")%></th>
        <th><%=IUDICO.Statistics.Localization.getMessage("Created")%></th>
     </tr>

     <% foreach (IUDICO.Common.Models.Shared.Curriculum curr in Model)
        { %>
        <tr>
            <td>
            <input type="checkbox" name="selectDisciplineId" value="<%: curr.Id %>" id="<%: curr.Id %>" />
            </td>
            <%--<td>
            <%: curr.Id %>
            </td>--%>
            <td>
            <%: curr.Name%>
            </td>
            <td>
            <%: curr.Created%>
            </td>
        </tr>
     <% } %>

     </table>

     <input type="button" value=<%=IUDICO.Statistics.Localization.getMessage("Show") %> onclick="checkBox();" />

     </form>

     </fieldset>

     <% } %>
     <% else { %>

     <h2> No curricuulm has been created for <%: ViewData["Group"]%>. </h2>

     <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">


</asp:Content>
