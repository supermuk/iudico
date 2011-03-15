<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.AllSpecializedResults>" %>
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
    <legend>Please, select one curriculum : </legend>

    <form action="/Stats/ThemesInfo/" method="post">
    <table>
    <tr>
        <th>Student</th>
        <% foreach (IUDICO.Common.Models.Curriculum curr in Model.Curriculums)
           {%>
           <th>
           <input type="radio" name="CurriculumID" value="<%: curr.Id %>" checked="checked" />
           <%: curr.Name %>
           </th>
        <% } %>
        <th>Sum</th>
        <th>Percent</th>
        <th>ECTS</th>
    </tr>
    
    <% foreach (IUDICO.Statistics.Models.Storage.SpecializedResult specializedResult in Model.SpecializedResult)
       { %>
       <tr>
            <td>
           <%: specializedResult.User.Username %>
            </td>
           
            <% foreach (IUDICO.Statistics.Models.Storage.CurriculumResult currResult in specializedResult.CurriculumResult)
            { %>
                <td>
                <%: currResult.Sum  %>
                /
                <%: currResult.Max  %>
                </td>     
            <% } %>
           
            <td>
                <%: specializedResult.Sum %>
                /
                <%: specializedResult.Max %>
            </td>
            <td>
                <%: specializedResult.Percent %>
                %
            </td>
            <td>
                <%: specializedResult.ECTS %>
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
