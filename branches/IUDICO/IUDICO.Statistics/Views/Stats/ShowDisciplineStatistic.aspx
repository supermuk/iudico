<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.AllSpecializedResults>" %>

<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Import Namespace="IUDICO.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("DisciplinesStatistic")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model.Users.Count() != 0)
       { %>
    <h2>
        <%=Localization.GetMessage("DisciplinesStatisticForGroup") %>
        <%: (ViewData["selectGroupName"])%>
    </h2>
    <%: Html.ActionLink(Localization.GetMessage("Back"), "Index")%>
    <fieldset>
        <legend>
            <%=Localization.GetMessage("SelectOneDiscipline")%>
        </legend>
        <table id="disciplinesTable">
            <thead>
                <tr>
                    <th>
                        <%=Localization.GetMessage("Student")%>
                    </th>
                    <% int i = 0;
                       foreach (var curriculum in Model.Curriculums)
                       {
                           i++;%>
                    <th>
                        <form name="linkform<%:i%>" action="/Stats/TopicsInfo/" method="post">
                        <input type="hidden" name="curriculumId" value="<%: curriculum.Id%>" />
                        </form>
                        <a href="javascript:document.forms['linkform<%:i%>'].submit();">
                            <%:curriculum.Discipline.Name%>
                        </a>
                    </th>
                    <% } %>
                    <th>
                        <%=Localization.GetMessage("Sum")%>
                    </th>
                    <th>
                        <%=Localization.GetMessage("Percent")%>
                    </th>
                    <th>
                        ECTS
                    </th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var specializedResult in Model.SpecializedResults)
                   { %>
                <tr>
                    <td>
                        [<%: specializedResult.User.Username %>]
                        <%: specializedResult.User.Name %>
                    </td>
                    <% foreach (var disciplineResult in specializedResult.DisciplineResults)
                       { %>
                    <td>
                        <%: Math.Round(Double.Parse(disciplineResult.Sum.ToString()), 2)%>
                        /
                        <%: disciplineResult.Max%>
                    </td>
                    <% } %>
                    <td>
                        <%:Math.Round(Double.Parse(specializedResult.Sum.ToString()),2) %>
                        /
                        <%: specializedResult.Max %>
                    </td>
                    <td>
                        <%: Math.Round(Double.Parse(specializedResult.Percent.ToString()))%>
                        %
                    </td>
                    <td>
                        <%: specializedResult.ECTS %>
                    </td>
                </tr>
                <% } %>
            </tbody>
        </table>
    </fieldset>
    <% } %>
    <% else
        { %>
    <h2>
        No student has been added for
        <%: ViewData["selectGroupName"]%>.
    </h2>
    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#disciplinesTable").dataTable({
                "bJQueryUI": true,
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": true,
                "bSort": false,
                "bInfo": false,
                "bAutoWidth": true
            });
        });
    </script>
    <style type="text/css">
        .dataTables_wrapper
        {
        	min-height: 100px;
        }
        
        /* styling link inside table header */
        div.DataTables_sort_wrapper a:link
        {
            color: #fffff0;
            text-decoration: underline;
        }
        div.DataTables_sort_wrapper a:visited
        {
            color: #f8f8ff;
        }
        div.DataTables_sort_wrapper a:hover
        {
            color: #ffffff;
            text-decoration: none;
        }
        div.DataTables_sort_wrapper a:active
        {
            color: #12eb87;
        }
    </style>
</asp:Content>
