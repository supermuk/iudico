<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.StatisticsStorage>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ShowCurriculumStatistic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Curriculums statistic for group
    <%: (ViewData["selectGroupName"])%>
    </h2>

    <%: Html.ActionLink("<- back", "Index")%>
    <fieldset>
    <legend>Please, select one group : </legend>

    <form action="/Stats/ThemesInfo/" method="post">
    <table>
    <tr>
        <th>Student</th>
        <% foreach (IUDICO.Common.Models.Curriculum curr in ViewData["Curriculums"] as IEnumerable<IUDICO.Common.Models.Curriculum>)
           {%>
           <th>
           <input type="radio" name="CurriculumID" value="<%: curr.Id %>"/>
           <%: curr.Name %>
           </th>
        <% } %>
        <th>Sum</th>
        <th>Percent</th>
        <th>ECTS</th>
    </tr>
    
    <% foreach (IUDICO.Common.Models.User user in ViewData["Students"] as IEnumerable<IUDICO.Common.Models.User>)
       { %>
       <tr>
           <td>
           <%: user.Username %>
           </td>
           <% int i=0; %>
           <% double? sumPoint = 0.0; int sumMax = 0; %>
           <% foreach (IUDICO.Common.Models.Curriculum c in ViewData["Curriculums"] as IEnumerable<IUDICO.Common.Models.Curriculum>)
           {%>
           <td>  
                <% foreach (KeyValuePair<List<IUDICO.Common.Models.Theme>, int> themeAndCurrId in ViewData["Themes"] as List<KeyValuePair<List<Theme>, int>>)
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
                <td>
                <% if((sumPoint/(double)sumMax*100.0) >= 91.0) {%>
                A <%} %>
                <% else if(sumPoint/(double)sumMax*100.0 >= 81.0) {%>
                B <%} %>
                <% else if(sumPoint/(double)sumMax*100.0 >= 71.0) {%>
                C <%} %>
                <% else if(sumPoint/(double)sumMax*100.0 >= 61.0) {%>
                D <%} %>
                <% else if(sumPoint/(double)sumMax*100.0 >= 51.0) {%>
                E <%} %>
                <% else if(sumPoint/(double)sumMax*100.0 >= 31.0) {%>
                F <%} %>
                <% else  {%>
                FX <%} %>
                </td>
       </tr>
    <% } %>
    </table>
    <input type="submit" value="Show" />

    </form>

    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
