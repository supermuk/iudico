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

    <% if (Model.Count() != 0)
       { %>

    <h2><%=IUDICO.Statistics.Localization.getMessage("CurriculumList")%> <%: ViewData["Group"]%> </h2>


     <%: Html.ActionLink(IUDICO.Statistics.Localization.getMessage("Back"), "Index")%>
     <fieldset>

     <legend><%=IUDICO.Statistics.Localization.getMessage("SelectCurriculum")%> </legend>


    <form id="curform" action="/Stats/ShowCurriculumStatistic/" method="post">
     <table>
     
     <tr>
        <th> </th>
        <%--<th>Curriculum id</th>--%>
        <th><%=IUDICO.Statistics.Localization.getMessage("CurriculumName")%></th>
        <th><%=IUDICO.Statistics.Localization.getMessage("Created")%></th>
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
