<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Curriculum>>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("Statistics")%>: <%=Localization.GetMessage("DisciplineList")%> <%: ViewData["Group"]%>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        function checkBox() {
            if ($('input:checkbox:checked').length == 0) {
                alert('<%=Localization.GetMessage("SelectDiscipline")%>')
            }
            else {
                $('#curform').submit();
            }
        }
    </script>

    <% if (Model.Count() != 0)
       { %>

    <h2><%=Localization.GetMessage("DisciplineList")%> <%: ViewData["Group"]%> </h2>


     <%: Html.ActionLink(Localization.GetMessage("Back"), "Index")%>
     <fieldset>

     <legend><%=Localization.GetMessage("SelectDiscipline")%>: </legend>


    <form id="curform" action="/Stats/ShowDisciplineStatistic/" method="post">
     <table>
     
     <tr>
        <th> </th>
        <%--<th>Discipline id</th>--%>
        <th><%=Localization.GetMessage("DisciplineName")%></th>
        <th><%=Localization.GetMessage("Created")%></th>
     </tr>

     <% foreach (var curriculum in Model)
        { %>
        <tr>
            <td>
            <input type="checkbox" name="selectedCurriculumId" value="<%: curriculum.Id %>" id="<%: curriculum.Id %>" />
            </td>
            <%--<td>
            <%: curr.Id %>
            </td>--%>
            <td>
            <%: curriculum.Discipline.Name%>
            </td>
            <td>
            <%: curriculum.Discipline.Created%>
            </td>
        </tr>
     <% } %>

     </table>

     <input type="button" value=<%=Localization.GetMessage("Show") %> onclick="checkBox();" />

     </form>

     </fieldset>

     <% } %>
     <% else { %>

     <h2> No curricuulm has been created for <%: ViewData["Group"]%>. </h2>

     <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">


</asp:Content>
