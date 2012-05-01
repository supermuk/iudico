<%@ Assembly Name="IUDICO.Statistics" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Curriculum>>" %>

<%@ Import Namespace="IUDICO.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("Statistics")%>:
    <%=Localization.GetMessage("DisciplineList")%>
    <%: ViewData["Group"]%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model.Count() != 0)
       { %>
    <h2>
        <%=Localization.GetMessage("DisciplineList")%>
        <%: ViewData["Group"]%>
    </h2>
    <%: Html.ActionLink(Localization.GetMessage("Back"), "Index")%>
    <fieldset>
        <legend>
            <%=Localization.GetMessage("SelectDiscipline")%>: </legend>
        <form id="curform" action="/Stats/ShowDisciplineStatistic/" method="post">
        <table id="curriculumsTable">
            <thead>
                <tr>
                    <th>
                    </th>
                    <th>
                        <%=Localization.GetMessage("DisciplineName")%>
                    </th>
                    <th>
                        <%=Localization.GetMessage("Created")%>
                    </th>
                </tr>
            </thead>
            <tbody>
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
            </tbody>
        </table>
        <br />
        <input type="button" class="submit_button" value='<%=Localization.GetMessage("Show") %>'
            onclick="checkBox();" />
        </form>
    </fieldset>
    <% } %>
    <% else
        { %>
    <h2>
        <%=Localization.GetMessage("NoСurriculumHasBeenCreatedFor")%>
        <%: ViewData["Group"]%>.
    </h2>
    <%} %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#curriculumsTable").dataTable({
                "bJQueryUI": true,
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": true,
                "bSort": false,
                "bInfo": false,
                "bAutoWidth": true
            });
        });

        function checkBox() {
            if ($('input:checkbox:checked').length == 0) {
                alert('<%=Localization.GetMessage("SelectDiscipline")%>');
            }
            else {
                $('#curform').submit();
            }
        }
    </script>

    <style type="text/css">
        .dataTables_wrapper
        {
        	min-height: 100px;
        }
    </style>
</asp:Content>
