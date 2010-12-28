<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.StatisticsStorage>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ShowCurriculumStatistic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
    <legend>Please, select one group : </legend>

    <table>
    <tr>
        <th>Student</th>
        <% foreach (IUDICO.Common.Models.Curriculum curr in Model.Curriculums)
           {%>
           <th>
           <%: curr.Name %>
           </th>
        <% } %>
        <th>Sum</th>
        <th>Percent</th>
    </tr>
    
    <% foreach (IUDICO.Common.Models.User user in Model.Students)
       { %>
       <tr>
           <td>
           <%: user.Username %>
           </td>
           <% int i=0; %>
           <% double? sumPoint = 0.0; int sumMax = 0; %>
           <% foreach (IUDICO.Common.Models.Curriculum c in Model.Curriculums)
           {%>
           <td>  
                <% foreach (KeyValuePair<List<IUDICO.Common.Models.Theme>, int> themeAndCurrId in Model.Themes)
                { %>
                
                <% if (themeAndCurrId.Value == c.Id)  { %>
                    
                    <% double? point=0.0; int max=0; %>
                    <% foreach (IUDICO.Common.Models.Theme th in themeAndCurrId.Key)
                       { %>

                       <% point += (double?)(ViewData["points"] as List<KeyValuePair<KeyValuePair<User, Theme>, double?>>).Where(x => x.Key.Key == user & x.Key.Value == th).Select(x => x.Value).First(); %>
                       <% max += 100; %>
                                
                    <% } %>

                    <% sumPoint += point; %>
                    <% sumMax += max; %>

                    <%: point %>
                    /
                    <%: max %>

                <% } %>
                <% } %>
                <% i++; %>
           </td>
           <% } %>
                <td>
                <%: sumPoint %>
                /
                <%: sumMax %>
                </td>
                <td>
                <%: sumPoint/(double)sumMax*100.0 %> %
                </td>
       </tr>
    <% } %>
    </table>

    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
